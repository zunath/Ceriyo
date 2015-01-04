using Ceriyo.Data.Enumerations;
using Ceriyo.Data.EventArguments;
using Ceriyo.Data.Server;
using Ceriyo.Data.Settings;
using Lidgren.Network;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Ceriyo.Library.Global;
using Ceriyo.Library.Network;
using Ceriyo.Library.Network.Packets;

namespace Ceriyo.Server
{
    public class ServerNetworkManager
    {
        private Dictionary<NetConnection, ServerPlayer> Players { get; set; }
        private ServerSettings Settings { get; set; }

        public ServerNetworkManager(string serverPassword, int port)
        {
            CeriyoServices.Agent = new NetworkAgent(NetworkAgentRoleEnum.Server, serverPassword, port);
            Players = new Dictionary<NetConnection, ServerPlayer>();
            CeriyoServices.Agent.OnConnected += Agent_OnConnected;
            CeriyoServices.Agent.OnDisconnected += Agent_OnDisconnected;
            CeriyoServices.Agent.OnDisconnecting += Agent_OnDisconnecting;
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
            List<PacketBase> packets = CeriyoServices.Agent.CheckForPackets();
            NetworkTransferData data = new NetworkTransferData
            {
                Players = Players,
                Settings = Settings
            };

            foreach (PacketBase packet in packets)
            {
                data = packet.Receive(data);
            }
        }

        public void Destroy()
        {
            CeriyoServices.Agent.Shutdown();

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

            CeriyoServices.Agent.SendPacket(packet, e.Connection, NetDeliveryMethod.ReliableUnordered);
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
