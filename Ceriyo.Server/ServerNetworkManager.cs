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
using Ceriyo.Data.NetworkObjects;

namespace Ceriyo.Server
{
    public class ServerNetworkManager
    {
        private GameModule Module { get; set; }
        private NetworkAgent Agent { get; set; }
        private Dictionary<NetConnection, string> PlayerUsernames { get; set; }
        public event EventHandler<PacketEventArgs> OnPacketReceived;

        public ServerNetworkManager(int port)
        {
            Agent = new NetworkAgent(NetworkAgentRoleEnum.Server, port);
            PlayerUsernames = new Dictionary<NetConnection, string>();
            Agent.OnConnected += Agent_OnConnected;
            Agent.OnDisconnected += Agent_OnDisconnected;
            Agent.OnDisconnecting += Agent_OnDisconnecting;
        }

        public void Update()
        {
            ProcessPackets();
            SendUpdatesToPlayers();
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
            // TODO: Send game state updates to players 
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

                UserConnectedPacket response = new UserConnectedPacket
                {
                    PlayerList = new List<PlayerNO>(), // TODO: Get Player List network object
                    Announcement = string.Empty // TODO: Get the server announcement
                };

                Agent.SendPacket(response, packet.SenderConnection, NetDeliveryMethod.ReliableUnordered);
            }
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
