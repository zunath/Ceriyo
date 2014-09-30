using Ceriyo.Data.Enumerations;
using Ceriyo.Data.Packets;
using Ceriyo.Library.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ceriyo.Data.EventArguments;
using Lidgren.Network;
using Ceriyo.Data.GameObjects;
using System.ComponentModel;
using Ceriyo.Data.Settings;
using Ceriyo.Data;
using System.IO;
using Ceriyo.Data.Engine;

namespace Ceriyo.Server
{
    public class ServerNetworkManager
    {
        private NetworkAgent Agent { get; set; }
        private Dictionary<NetConnection, string> PlayerUsernames { get; set; }
        public event EventHandler<PacketEventArgs> OnPacketReceived;
        private ServerSettings Settings { get; set; }
        private WorkingDataManager WorkingManager { get; set; }
        private EngineDataManager EngineManager { get; set; }

        public ServerNetworkManager(string serverPassword, int port)
        {
            Agent = new NetworkAgent(NetworkAgentRoleEnum.Server, serverPassword, port);
            PlayerUsernames = new Dictionary<NetConnection, string>();
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
            this.Settings = settings;
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
                else if (type == typeof(CharacterCreationPacket))
                {
                    ReceiveCharacterCreationPacket(packet as CharacterCreationPacket);
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
            return new BindingList<string>((from name
                                            in PlayerUsernames.Values
                                            select name).ToList());
        }

        #region Packet Processing

        private void ReceiveUserInfoPacket(UserInfoPacket packet)
        {
            if (!PlayerUsernames.ContainsKey(packet.SenderConnection) && 
                !PlayerUsernames.ContainsValue(packet.Username) &&
                !packet.IsRequest)
            {
                PlayerUsernames.Add(packet.SenderConnection, packet.Username);

                if (!Directory.Exists(EnginePaths.CharactersDirectory + packet.Username))
                {
                    Directory.CreateDirectory(EnginePaths.CharactersDirectory + packet.Username);
                }

                List<Player> characters = EngineManager.GetPlayers(packet.Username);
                
                UserConnectedPacket response = new UserConnectedPacket
                {
                    CharacterList = characters, 
                    Announcement = Settings.Announcement,
                    CanDeleteCharacters = Settings.AllowCharacterDeletion
                };

                Agent.SendPacket(response, packet.SenderConnection, NetDeliveryMethod.ReliableUnordered);
            }
        }

        private void ReceiveDeleteCharacterPacket(DeleteCharacterPacket packet)
        {
            // TODO: Delete the player character file.

            DeleteCharacterPacket response = new DeleteCharacterPacket
            {
                IsDeleteSuccessful = true
            };

            Agent.SendPacket(response, packet.SenderConnection, NetDeliveryMethod.ReliableUnordered);
        }

        private void ReceiveCharacterCreationPacket(CharacterCreationPacket packet)
        {
            CharacterCreationPacket response = new CharacterCreationPacket
            {
                Abilities = WorkingManager.GetAllGameObjects<Ability>(ModulePaths.AbilitiesDirectory).ToList(),
                CharacterClasses = WorkingManager.GetAllGameObjects<CharacterClass>(ModulePaths.CharacterClassesDirectory).ToList(),
                Races = WorkingManager.GetAllGameObjects<Race>(ModulePaths.RacesDirectory).ToList(),
                Skills = WorkingManager.GetAllGameObjects<Skill>(ModulePaths.SkillsDirectory).ToList()
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
            if (PlayerUsernames.ContainsKey(e.Connection))
            {
                PlayerUsernames.Remove(e.Connection);
            }
        }

        #endregion
    }
}
