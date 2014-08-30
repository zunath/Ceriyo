﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Ceriyo.Data.Enumerations;
using Ceriyo.Data.EventArguments;
using Ceriyo.Data.Packets;
using Lidgren.Network;
using ProtoBuf;
using Ceriyo.Data;

namespace Ceriyo.Library.Network
{
    public class NetworkAgent
    {
        public event EventHandler<ConnectionStatusEventArgs> OnConnected;
        public event EventHandler<ConnectionStatusEventArgs> OnDisconnecting;
        public event EventHandler<ConnectionStatusEventArgs> OnDisconnected;
        
        private NetPeer Peer { get; set; }
        private NetPeerConfiguration Configuration { get; set; }
        private NetworkAgentRoleEnum Role { get; set; }
        public int Port { get; private set; }
        private NetOutgoingMessage OutgoingMessage { get; set; }
        private List<NetIncomingMessage> IncomingMessages { get; set; }
        private NetXtea Encryption { get; set; }
        private string ServerPassword { get; set; }

        public List<NetConnection> Connections
        {
            get
            {
                return Peer.Connections;
            }
        }

        public NetworkAgent(NetworkAgentRoleEnum role, string serverPassword = null, int port = 5121)
        {
            Role = role;
            Configuration = new NetPeerConfiguration(EngineConstants.ApplicationIdentifier);
            Port = port;
            ServerPassword = serverPassword == null ? string.Empty : serverPassword;

            Initialize();
        }

        private void Initialize()
        {
            Encryption = new NetXtea(EngineConstants.PacketEncryptionKey);

            if (Role == NetworkAgentRoleEnum.Server)
            {
                Configuration.EnableMessageType(NetIncomingMessageType.DiscoveryRequest);
                Configuration.Port = Port;
                Configuration.SetMessageTypeEnabled(NetIncomingMessageType.ConnectionApproval, true);

                //Casts the NetPeer to a NetServer
                Peer = new NetServer(Configuration);
            }

            else if (Role == NetworkAgentRoleEnum.Client)
            {
                Configuration.EnableMessageType(NetIncomingMessageType.DiscoveryResponse);
                //Casts the NetPeer to a NetClient
                Peer = new NetClient(Configuration);
            }

            Peer.Start();

            IncomingMessages = new List<NetIncomingMessage>();
            OutgoingMessage = Peer.CreateMessage();
        }

        /// <summary>
        /// Connects to a server. Throws an exception if you attempt to call Connect as a Server.
        /// </summary>
        public NetConnection Connect(string ip, string serverPassword)
        {
            if (Role == NetworkAgentRoleEnum.Client)
            {
                NetOutgoingMessage hailMessage = Peer.CreateMessage(serverPassword);
                hailMessage.Encrypt(Encryption);

                IncomingMessages.Clear(); // Remove any old, out of date packets from being processed.
                return Peer.Connect(ip, Port, hailMessage);
            }
            else
            {
                throw new SystemException("Attempted to connect as server. Only clients should connect.");
            }
        }

        /// <summary>
        /// Disconnects from all connections
        /// </summary>
        public void Disconnect()
        {
            foreach (NetConnection connection in Connections)
            {
                connection.Disconnect("Disconnecting");
            }
        }

        public void Shutdown()
        {
            Peer.Shutdown("Shutting down server");
        }

        private void WriteMessage(PacketBase packet)
        {
            MemoryStream stream = new MemoryStream();
            Serializer.Serialize<PacketBase>(stream, packet); // Protobuf serialization
            OutgoingMessage.Write(stream.ToArray());
        }

        private void SendMessage(NetConnection recipient, NetDeliveryMethod method, int sequenceChannel)
        {
            if (recipient != null)
            {
                OutgoingMessage.Encrypt(Encryption);
                Peer.SendMessage(OutgoingMessage, recipient, method, sequenceChannel);
                OutgoingMessage = Peer.CreateMessage();
            }
        }

        private List<NetIncomingMessage> CheckForMessages()
        {
            IncomingMessages.Clear();
            NetIncomingMessage incomingMessage;

            while ((incomingMessage = Peer.ReadMessage()) != null)
            {
                NetConnection senderConnection = incomingMessage.SenderConnection;
                switch (incomingMessage.MessageType)
                {
                    case NetIncomingMessageType.ConnectionApproval:
                        {
                            string serverPassword = string.Empty;
                            incomingMessage.SenderConnection.RemoteHailMessage.Decrypt(Encryption);
                            if (incomingMessage.SenderConnection.RemoteHailMessage != null)
                            {
                                serverPassword = incomingMessage.SenderConnection.RemoteHailMessage.ReadString();
                            }

                            if (serverPassword == ServerPassword || string.IsNullOrWhiteSpace(ServerPassword))
                            {
                                senderConnection.Approve();
                            }
                            else
                            {
                                senderConnection.Deny("Invalid password.");
                            }
                        }
                        break;
                    case NetIncomingMessageType.DiscoveryRequest:
                        Peer.SendDiscoveryResponse(null, incomingMessage.SenderEndPoint);
                        break;
                    case NetIncomingMessageType.VerboseDebugMessage:
                    case NetIncomingMessageType.DebugMessage:
                    case NetIncomingMessageType.WarningMessage:
                    case NetIncomingMessageType.ErrorMessage:
                        break;
                    case NetIncomingMessageType.StatusChanged:
                        {

                            NetConnectionStatus status = (NetConnectionStatus)incomingMessage.ReadByte();
                            ConnectionStatusEventArgs e = new ConnectionStatusEventArgs
                            {
                                Connection = incomingMessage.SenderConnection
                            };
                            
                            if (status == NetConnectionStatus.Connected)
                            {
                                if (OnConnected != null)
                                {
                                    OnConnected(this, e);
                                }
                            }
                            else if (status == NetConnectionStatus.Disconnecting)
                            {
                                if(OnDisconnecting != null)
                                {
                                    OnDisconnecting(this, e);
                                }
                            }
                            else if (status == NetConnectionStatus.Disconnected)
                            {
                                if(OnDisconnected != null)
                                {
                                    OnDisconnected(this, e);
                                }
                            }

                            break;
                        }
                    case NetIncomingMessageType.Data:
                        incomingMessage.Decrypt(Encryption);
                        IncomingMessages.Add(incomingMessage);
                        break;
                    default:
                        // unknown message type
                        break;
                }
            }
            return IncomingMessages;
        }

        public List<PacketBase> CheckForPackets(string newPassword = null)
        {
            if (newPassword != null)
            {
                this.ServerPassword = newPassword;
            }

            List<NetIncomingMessage> messages = CheckForMessages();
            List<PacketBase> packets = new List<PacketBase>();
            MemoryStream stream = new MemoryStream();
            PacketBase currentPacket;

            foreach (NetIncomingMessage currentMessage in messages)
            {
                stream = new MemoryStream(currentMessage.ReadBytes(currentMessage.LengthBytes));
                currentPacket = Serializer.Deserialize<PacketBase>(stream); // Protobuf deserialization
                currentPacket.SenderConnection = currentMessage.SenderConnection;

                packets.Add(currentPacket);
            }

            return packets;
        }

        public void SendPacket(PacketBase packet, NetConnection connection, NetDeliveryMethod deliveryMethod, int sequenceChannel = 1)
        {
            WriteMessage(packet);
            SendMessage(connection, deliveryMethod, sequenceChannel);
        }

    }
}
