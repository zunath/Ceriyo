using System;
using System.Collections.Generic;
using System.IO;
using Ceriyo.Core.Constants;
using Ceriyo.Core.Contracts;
using Ceriyo.Core.Packets;
using Ceriyo.Core.Services.Contracts;
using Ceriyo.Core.Settings;
using Lidgren.Network;
using ProtoBuf;

namespace Ceriyo.Infrastructure.Services
{
    public class ServerNetworkService: IServerNetworkService
    {
        private NetServer _server;
        private readonly ILogger _logger;
        private readonly IEngineService _engineService;
        private readonly Dictionary<string, NetConnection> _connections;
        private readonly ServerSettings _serverSettings;

        public ServerNetworkService(ILogger logger, IEngineService engineService, ServerSettings serverSettings)
        {
            _logger = logger;
            _engineService = engineService;
            _serverSettings = serverSettings;
            _connections = new Dictionary<string, NetConnection>();
        }

        public void StartServer(int port)
        {
            NetPeerConfiguration config = new NetPeerConfiguration(_engineService.ApplicationIdentifier)
            {
                Port = port
            };
            config.EnableMessageType(NetIncomingMessageType.ConnectionApproval);

            _server = new NetServer(config);
            _server.Start();
            _connections.Clear();
        }

        public void StopServer()
        {
            if (_server == null ||
                _server.Status == NetPeerStatus.NotRunning ||
                _server.Status == NetPeerStatus.ShutdownRequested)
            {
                throw new Exception("Server is currently not running.");
            }

            _server.Shutdown("Server shutting down.");
            _connections.Clear();
        }

        public void ProcessMessages()
        {
            NetIncomingMessage message;
            while ((message = _server.ReadMessage()) != null)
            {
                switch (message.MessageType)
                {
                    case NetIncomingMessageType.VerboseDebugMessage:
                    case NetIncomingMessageType.DebugMessage:
                    case NetIncomingMessageType.WarningMessage:
                        _logger.Info(message.ReadString());
                        break;
                    case NetIncomingMessageType.ErrorMessage:
                    case NetIncomingMessageType.Error:
                        _logger.Error(message.ReadString());
                        break;

                    case NetIncomingMessageType.Data:
                        break;
                        
                    case NetIncomingMessageType.ConnectionApproval:
                        HandleConnectionApproval(message);
                        break;

                    default:
                        _logger.Error("Message type unhandled.");
                        break;
                }

                _server.Recycle(message);
            }
        }

        private void HandleConnectionApproval(NetIncomingMessage message)
        {
            MemoryStream stream = new MemoryStream(message.Data);
            var packet = Serializer.Deserialize<ConnectionRequestPacket>(stream);
            if (!string.IsNullOrWhiteSpace(_serverSettings.PlayerPassword))
            {
                if (packet.Password == _serverSettings.PlayerPassword)
                {
                    message.SenderConnection.Approve();
                    _connections.Add(packet.Username, message.SenderConnection);
                }
                else
                {
                    message.SenderConnection.Deny("Invalid password.");
                }
            }
            else
            {
                message.SenderConnection.Approve();
                _connections.Add(packet.Username, message.SenderConnection);
            }

        }

        public void SendMessage(PacketDeliveryMethod method, INetworkPacket packet, string accountName)
        {
            if (!_connections.ContainsKey(accountName)) return;
            NetConnection recipient = _connections[accountName];

            NetOutgoingMessage message = _server.CreateMessage();
            MemoryStream stream = new MemoryStream();
            Serializer.Serialize(stream, packet);
            message.Write(stream.ToArray());
            
            // This may seem redundant but I am protecting against the possibility that
            // Lidgren is vaporware. Should we need to swap out the network code in the future,
            // only this class will need to be touched.
            NetDeliveryMethod deliveryMethod = NetDeliveryMethod.Unreliable;
            switch (method)
            {
                case PacketDeliveryMethod.Unreliable:
                    deliveryMethod = NetDeliveryMethod.Unreliable;
                    break;
                case PacketDeliveryMethod.UnreliableSequenced:
                    deliveryMethod = NetDeliveryMethod.UnreliableSequenced;
                    break;
                case PacketDeliveryMethod.ReliableUnordered:
                    deliveryMethod = NetDeliveryMethod.ReliableUnordered;
                    break;
                case PacketDeliveryMethod.ReliableSequenced:
                    deliveryMethod = NetDeliveryMethod.ReliableSequenced;
                    break;
                case PacketDeliveryMethod.ReliableOrdered:
                    deliveryMethod = NetDeliveryMethod.ReliableOrdered;
                    break;
            }


            _server.SendMessage(message, recipient, deliveryMethod);
        }
    }
}
