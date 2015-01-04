using Ceriyo.Data.Enumerations;
using Ceriyo.Data.EventArguments;
using Ceriyo.Data.Server;
using Ceriyo.Data.Settings;
using Lidgren.Network;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Ceriyo.Network;
using Ceriyo.Network.Packets;

namespace Ceriyo.Server
{
    public class ServerNetworkManager
    {
        private NetworkAgent Agent { get; set; }
        private Dictionary<NetConnection, ServerPlayer> Players { get; set; }
        private ServerSettings Settings { get; set; }

        public ServerNetworkManager(string serverPassword, int port)
        {
            Agent = new NetworkAgent(NetworkAgentRoleEnum.Server, serverPassword, port);
            Players = new Dictionary<NetConnection, ServerPlayer>();
            Agent.OnConnected += Agent_OnConnected;
            Agent.OnDisconnected += Agent_OnDisconnected;
            Agent.OnDisconnecting += Agent_OnDisconnecting;
        }

        public void Update()
        {
            ProcessPackets();
        }

        public void RefreshSettings(ServerSettings settings)
        {
            Settings = settings;
        }

        private void ProcessPackets()
        {
            List<PacketBase> packets = Agent.CheckForPackets();
            ServerNetworkData data = new ServerNetworkData
            {
                Players = Players,
                Settings = Settings
            };

            foreach (PacketBase packet in packets)
            {
                data = packet.Receive(data);

                if (data.ResponsePacket != null)
                {
                    Agent.SendPacket(data.ResponsePacket, packet.SenderConnection, data.DeliveryMethod);

                    // Reset for next packet.
                    data.ResponsePacket = null;
                    data.DeliveryMethod = NetDeliveryMethod.Unreliable;
                }
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

        #endregion
    }
}
