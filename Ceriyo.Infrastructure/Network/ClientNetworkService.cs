using System;
using System.IO;
using Ceriyo.Core.Constants;
using Ceriyo.Core.Contracts;
using Ceriyo.Core.Services.Contracts;
using Ceriyo.Infrastructure.Network.Contracts;
using Ceriyo.Infrastructure.Network.Packets;
using Lidgren.Network;
using ProtoBuf;

namespace Ceriyo.Infrastructure.Network
{
    public class ClientNetworkService: IClientNetworkService
    {
        private readonly NetClient _client;
        private readonly ILogger _logger;
        private NetConnection _serverConnection;

        public ClientNetworkService(ILogger logger, IEngineService engineService)
        {
            _logger = logger;
            NetPeerConfiguration config = new NetPeerConfiguration(engineService.ApplicationIdentifier);
            _client = new NetClient(config);
            _client.Start();
        }

        public void ConnectToServer(string ipAddress, int port, string username, string password)
        {
            if (_serverConnection != null && _serverConnection.Status == NetConnectionStatus.Connected)
                throw new Exception("Client is already connected to a server.");

            ConnectionRequestPacket packet = new ConnectionRequestPacket
            {
                Username = username,
                Password = password
            };

            NetOutgoingMessage message = _client.CreateMessage();
            MemoryStream stream = new MemoryStream();
            Serializer.Serialize(stream, packet);
            message.Write(stream.ToArray());

            _serverConnection = _client.Connect(ipAddress, port, message);
            _logger.Info($"Connecting to server: {ipAddress + ":" + port}");
        }

        public void DisconnectFromServer(string disconnectMessage = "")
        {
            if(_serverConnection == null) 
                throw new Exception("Client is not currently connected to a server.");
            
            _serverConnection.Disconnect(disconnectMessage);
            _logger.Info($"Disconnecting from server: {_serverConnection.RemoteEndPoint.Address + ":" + _serverConnection.RemoteEndPoint.Port}");
        }

        public void ProcessMessages()
        {
            if (_client == null) return;

            NetIncomingMessage message;
            while ((message = _client.ReadMessage()) != null)
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
                        MemoryStream stream = new MemoryStream(message.ReadBytes(message.LengthBytes));
                        PacketBase packet = Serializer.Deserialize<PacketBase>(stream);
                        PacketReceived?.Invoke(packet);
                        break;
                        
                    case NetIncomingMessageType.StatusChanged:
                        NetConnectionStatus status = (NetConnectionStatus)message.ReadByte();

                        if (status == NetConnectionStatus.Connected)
                        {
                            OnConnected?.Invoke();
                        }
                        else if (status == NetConnectionStatus.Disconnected)
                        {
                            OnDisconnected?.Invoke();
                        }

                        break;

                    default:
                        _logger.Error("Message type unhandled.");
                        break;
                }

                _client.Recycle(message);
            }
        }

        public void SendMessage(PacketDeliveryMethod method, INetworkPacket packet)
        {
            if (_serverConnection == null ||
                _serverConnection.Status != NetConnectionStatus.Connected)
            {
                throw new Exception("Client is not currently connected to a server.");
            }

            NetOutgoingMessage message = _client.CreateMessage();
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
            _client.SendMessage(message, deliveryMethod);
        }

        public event Action OnConnected;
        public event Action OnDisconnected;
        public event Action<PacketBase> PacketReceived;
        

    }
}
