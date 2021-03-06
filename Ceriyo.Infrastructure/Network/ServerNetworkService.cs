﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Ceriyo.Core.Constants;
using Ceriyo.Core.Contracts;
using Ceriyo.Core.Data;
using Ceriyo.Core.Services.Contracts;
using Ceriyo.Infrastructure.Network.Contracts;
using Ceriyo.Infrastructure.Network.Packets;
using Ceriyo.Infrastructure.Network.Packets.Connection;
using Ceriyo.Infrastructure.Network.TransferObjects;
using Lidgren.Network;
using ProtoBuf;

namespace Ceriyo.Infrastructure.Network
{
    public class ServerNetworkService: IServerNetworkService
    {
        private NetServer _server;
        private readonly ILogger _logger;
        private readonly IEngineService _engineService;
        private readonly IServerSettingsService _settingsService;
        private readonly Dictionary<string, NetConnection> _usernameToConnection;
        private readonly Dictionary<NetConnection, string> _connectionToUsername;
        private readonly IModuleService _moduleService;
        private readonly IPathService _pathService;
        private readonly IDataService _dataService;
        private readonly Dictionary<string, Tuple<Type, Action<string, PacketBase>>> _boundPacketActions;

        public ServerNetworkService(ILogger logger, 
            IEngineService engineService,
            IServerSettingsService settingsService,
            IModuleService moduleService,
            IPathService pathService,
            IDataService dataService)
        {
            _logger = logger;
            _engineService = engineService;
            _settingsService = settingsService;
            _moduleService = moduleService;
            _pathService = pathService;
            _dataService = dataService;
            _usernameToConnection = new Dictionary<string, NetConnection>();
            _connectionToUsername = new Dictionary<NetConnection, string>();

            _boundPacketActions = new Dictionary<string, Tuple<Type, Action<string, PacketBase>>>();
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
            _usernameToConnection.Clear();
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
            _usernameToConnection.Clear();

            _boundPacketActions.Clear();
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
                        string username = _connectionToUsername[message.SenderConnection];
                        MemoryStream stream = new MemoryStream(message.ReadBytes(message.LengthBytes));
                        PacketBase packet = Serializer.Deserialize<PacketBase>(stream);
                        ExecuteBoundActions(username, packet);
                        break;
                        
                    case NetIncomingMessageType.ConnectionApproval:
                        HandleConnectionApproval(message);
                        break;

                    case NetIncomingMessageType.StatusChanged:
                        NetConnectionStatus status = (NetConnectionStatus)message.ReadByte();

                        if (status == NetConnectionStatus.Connected)
                        {
                            HandleConnected(message);
                        }
                        else if (status == NetConnectionStatus.Disconnected)
                        {
                            HandleDisconnected(message);
                        }

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
            MemoryStream stream = new MemoryStream(message.ReadBytes(message.LengthBytes));
            var packet = Serializer.Deserialize<ConnectionRequestPacket>(stream);

            if (packet.Password != _settingsService.PlayerPassword)
            {
                message.SenderConnection.Deny("Invalid password.");
                _logger.Info($"User {packet.Username} entered an invalid password.");
                return;
            }

            if (_usernameToConnection.ContainsKey(packet.Username) || 
                _connectionToUsername.ContainsKey(message.SenderConnection))
            {
                message.SenderConnection.Deny("User is already connected.");
                _logger.Info($"User {packet.Username} is already connected to the server.");
                return;
            }

            if (_usernameToConnection.Count >= _settingsService.MaxPlayers)
            {
                message.SenderConnection.Deny("Server is full.");
                _logger.Info($"User {packet.Username} tried to connect but the server was already full.");
                return;
            }

            if (_settingsService.BlackList.Contains(packet.Username.ToLower()))
            {
                message.SenderConnection.Deny("You are banned from this server.");
                _logger.Info($"User {packet.Username} tried to connect but this user is banned from the server.");
                return;
            }


            message.SenderConnection.Approve();
            _usernameToConnection.Add(packet.Username, message.SenderConnection);
            _connectionToUsername.Add(message.SenderConnection, packet.Username);
        }

        private void HandleConnected(NetIncomingMessage message)
        {
            string username = _connectionToUsername[message.SenderConnection];

            int serverNameLength = _settingsService.ServerName.Length > 32 ? 32 : _settingsService.ServerName.Length;
            int announcementLength = _settingsService.Announcement.Length > 255 ? 255 : _settingsService.Announcement.Length;
            ConnectedToServerPacket response = new ConnectedToServerPacket
            {
                ServerName = _settingsService.ServerName.Substring(0, serverNameLength),
                AllowCharacterDeletion = _settingsService.AllowCharacterDeletion,
                Announcement = _settingsService.Announcement.Substring(0, announcementLength),
                Category = _settingsService.GameCategory,
                CurrentPlayers = _usernameToConnection.Count,
                MaxPlayers = _settingsService.MaxPlayers,
                PVP = _settingsService.PVPType,
                RequiredResourcePacks = _moduleService.GetLoadedModuleData().ResourcePacks
            };

            string directoryPath = _pathService.ServerVaultDirectory + "/" + username + "/";
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            string[] files = Directory.GetFiles(_pathService.ServerVaultDirectory + "/" + username + "/", "*.pcf");
            foreach (string pcFile in files)
            {
                PCData pc = _dataService.Load<PCData>(pcFile);
                PCTransferObject pcTO = PCTransferObject.Load(pc);

                response.PCs.Add(pcTO);
            }
            
            SendMessage(PacketDeliveryMethod.ReliableUnordered, response, username);

            OnPlayerConnected?.Invoke(username);
        }

        private void HandleDisconnected(NetIncomingMessage message)
        {
            string username = _connectionToUsername[message.SenderConnection];
            OnPlayerDisconnected?.Invoke(username);
            _usernameToConnection.Remove(username);
            _connectionToUsername.Remove(message.SenderConnection);
        }


        public void SendMessage(PacketDeliveryMethod method, INetworkPacket packet, string accountName)
        {
            if (!_usernameToConnection.ContainsKey(accountName)) return;
            NetConnection recipient = _usernameToConnection[accountName];

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

        public void BootUsername(string username)
        {
            if (!_usernameToConnection.ContainsKey(username)) return;

            NetConnection connection = _usernameToConnection[username];
            connection.Disconnect("You have been booted from the server.");
        }

        public string BindPacketAction<T>(Action<string, PacketBase> action)
            where T : INetworkPacket
        {
            string bindingID = Guid.NewGuid().ToString();

            Tuple<Type, Action<string, PacketBase>> tuple = new Tuple<Type, Action<string, PacketBase>>(typeof(T), action);

            _boundPacketActions.Add(bindingID, tuple);

            return bindingID;
        }

        public void UnbindPacketAction(string bindingID)
        {
            _boundPacketActions.Remove(bindingID);
        }

        private void ExecuteBoundActions(string username, PacketBase packet)
        {
            Type type = packet.GetType();

            var bindings = _boundPacketActions.Values.ToList();
            foreach (var binding in bindings)
            {
                if (binding.Item1 == type)
                {
                    binding.Item2(username, packet);
                }
            }


        }

        public event Action<string> OnPlayerConnected;
        public event Action<string> OnPlayerDisconnected;
    }
}
