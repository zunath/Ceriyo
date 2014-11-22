using Ceriyo.Data;
using Ceriyo.Data.Engine;
using Ceriyo.Data.Enumerations;
using Ceriyo.Data.EventArguments;
using Ceriyo.Data.GameObjects;
using Ceriyo.Data.Packets;
using Ceriyo.Data.Server;
using Ceriyo.Data.Settings;
using Ceriyo.Library.Network;
using Lidgren.Network;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;

namespace Ceriyo.Server
{
    public class ServerNetworkManager
    {
        private NetworkAgent Agent { get; set; }
        private Dictionary<NetConnection, ServerPlayer> Players { get; set; }
        public event EventHandler<PacketEventArgs> OnPacketReceived;
        private ServerSettings Settings { get; set; }
        private WorkingDataManager WorkingManager { get; set; }
        private EngineDataManager EngineManager { get; set; }

        public ServerNetworkManager(string serverPassword, int port)
        {
            Agent = new NetworkAgent(NetworkAgentRoleEnum.Server, serverPassword, port);
            Players = new Dictionary<NetConnection, ServerPlayer>();
            Agent.OnConnected += Agent_OnConnected;
            Agent.OnDisconnected += Agent_OnDisconnected;
            Agent.OnDisconnecting += Agent_OnDisconnecting;
            WorkingManager = new WorkingDataManager();
            EngineManager = new EngineDataManager();
        }

        public void Update()
        {
            ProcessPackets();
            SendUpdatesToPlayers();
        }

        public void RefreshSettings(ServerSettings settings)
        {
            Settings = settings;
        }

        private void ProcessPackets()
        {
            List<PacketBase> packets = Agent.CheckForPackets();

            foreach (PacketBase packet in packets)
            {
                Type type = packet.GetType();

                if (type == typeof(UserInfoPacket))
                {
                    ReceiveUserInfoPacket(packet as UserInfoPacket);
                }
                else if (type == typeof(DeleteCharacterPacket))
                {
                    ReceiveDeleteCharacterPacket(packet as DeleteCharacterPacket);
                }
                else if (type == typeof(CharacterCreationScreenPacket))
                {
                    ReceiveCharacterCreationScreenPacket(packet as CharacterCreationScreenPacket);
                }
                else if (type == typeof(CreateCharacterPacket))
                {
                    ReceiveCreateCharacterPacket(packet as CreateCharacterPacket);
                }
                else if (type == typeof(CharacterSelectionScreenPacket))
                {
                    ReceiveCharacterSelectionScreenPacket(packet as CharacterSelectionScreenPacket);
                }
                else if (type == typeof(SelectCharacterPacket))
                {
                    ReceiveSelectCharacterPacket(packet as SelectCharacterPacket);
                }
                else if (type == typeof(GameScreenPacket))
                {
                    ReceiveGameScreenPacket(packet as GameScreenPacket);
                }
                else
                {
                    if (OnPacketReceived != null)
                    {
                        OnPacketReceived(this, new PacketEventArgs(packet));
                    }
                }
            }
        }

        private void SendUpdatesToPlayers()
        {
            foreach (NetConnection connection in Agent.Connections)
            {
                // TODO: Send game state updates to players 
            }
        }

        public void Destroy()
        {
            Agent.Shutdown();

        }

        public BindingList<string> GetPlayerNames()
        {
            return new BindingList<string>((from pc
                                            in Players.Values
                                            select pc.Username).ToList());
        }

        #region Packet Processing

        private void ReceiveUserInfoPacket(UserInfoPacket packet)
        {
            if (!Players.ContainsKey(packet.SenderConnection) &&
                Players.SingleOrDefault(x => x.Value.Username == packet.Username).Value == null &&
                !packet.IsRequest)
            {
                ServerPlayer pc = new ServerPlayer
                {
                    PC = new Player(),
                    Username = packet.Username
                };

                Players.Add(packet.SenderConnection, pc);

                if (!Directory.Exists(EnginePaths.CharactersDirectory + packet.Username))
                {
                    Directory.CreateDirectory(EnginePaths.CharactersDirectory + packet.Username);
                }

                UserConnectedPacket response = new UserConnectedPacket
                {
                    IsSuccessful = true
                };

                Agent.SendPacket(response, packet.SenderConnection, NetDeliveryMethod.ReliableUnordered);
            }
        }

        private void ReceiveDeleteCharacterPacket(DeleteCharacterPacket packet)
        {
            bool success = false;

            if (Settings.AllowCharacterDeletion)
            {
                success = EngineManager.DeletePlayer(Players[packet.SenderConnection].Username, packet.CharacterResref);
            }

            DeleteCharacterPacket response = new DeleteCharacterPacket
            {
                IsDeleteSuccessful = success
            };

            Agent.SendPacket(response, packet.SenderConnection, NetDeliveryMethod.ReliableUnordered);
        }

        private void ReceiveCharacterCreationScreenPacket(CharacterCreationScreenPacket packet)
        {
            CharacterCreationScreenPacket response = new CharacterCreationScreenPacket
            {
                Abilities = WorkingManager.GetAllGameObjects<Ability>(ModulePaths.AbilitiesDirectory).ToList(),
                CharacterClasses = WorkingManager.GetAllGameObjects<CharacterClass>(ModulePaths.CharacterClassesDirectory).ToList(),
                Skills = WorkingManager.GetAllGameObjects<Skill>(ModulePaths.SkillsDirectory).ToList()
            };

            Agent.SendPacket(response, packet.SenderConnection, NetDeliveryMethod.ReliableUnordered);
        }

        private void ReceiveCreateCharacterPacket(CreateCharacterPacket packet)
        {
            Player pc = new Player
            {
                Name = packet.Name,
                Description = packet.Description
            };

            string username = Players[packet.SenderConnection].Username;
            EngineManager.SavePlayer(username, pc, true);

            CreateCharacterPacket response = new CreateCharacterPacket
            {
                ResponsePlayer = pc
            };

            Agent.SendPacket(response, packet.SenderConnection, NetDeliveryMethod.ReliableUnordered);
        }

        private void ReceiveCharacterSelectionScreenPacket(CharacterSelectionScreenPacket packet)
        {
            string username = Players[packet.SenderConnection].Username;
            List<Player> characters = EngineManager.GetPlayers(username);

            CharacterSelectionScreenPacket response = new CharacterSelectionScreenPacket
            {
                CharacterList = characters,
                Announcement = Settings.Announcement,
                CanDeleteCharacters = Settings.AllowCharacterDeletion
            };

            Agent.SendPacket(response, packet.SenderConnection, NetDeliveryMethod.ReliableUnordered);
        }

        private void ReceiveSelectCharacterPacket(SelectCharacterPacket packet)
        {
            string username = Players[packet.SenderConnection].Username;
            Player pc = EngineManager.GetPlayer(username, packet.Resref);
            
            SelectCharacterPacket response = new SelectCharacterPacket();

            if (pc != null)
            {
                Players[packet.SenderConnection].PC = pc;
                response.IsSuccessful = true;
            }

            Agent.SendPacket(response, packet.SenderConnection, NetDeliveryMethod.ReliableUnordered);
        }

        private void ReceiveGameScreenPacket(GameScreenPacket packet)
        {
            GameScreenPacket response = new GameScreenPacket
            {
                PC = Players[packet.SenderConnection].PC
            };

            Agent.SendPacket(response, packet.SenderConnection, NetDeliveryMethod.ReliableUnordered);
        }

        #endregion

        #region Network Connection

        private void Agent_OnConnected(object sender, ConnectionStatusEventArgs e)
        {
            UserInfoPacket packet = new UserInfoPacket
            {
                IsRequest = true
            };

            Agent.SendPacket(packet, e.Connection, NetDeliveryMethod.ReliableUnordered);
        }

        private void Agent_OnDisconnecting(object sender, ConnectionStatusEventArgs e)
        {

        }

        private void Agent_OnDisconnected(object sender, ConnectionStatusEventArgs e)
        {
            if (Players.ContainsKey(e.Connection))
            {
                Players.Remove(e.Connection);
            }
        }

        public void SendPacket(PacketBase packet, NetConnection connection, NetDeliveryMethod deliveryMethod, int sequenceChannel = 1)
        {
            Agent.SendPacket(packet, connection, deliveryMethod, sequenceChannel);
        }

        #endregion
    }
}
