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

namespace Ceriyo.Server
{
    public class ServerActivityProcessor
    {
        private GameModule Module { get; set; }
        private NetworkAgent Agent { get; set; }
        private List<Player> Players { get; set; }

        public ServerActivityProcessor(int port)
        {
            Agent = new NetworkAgent(NetworkAgentRoleEnum.Server, port);
            Players = new List<Player>();
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
                // TODO: Process packets.
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
            return new BindingList<string>((from player
                                            in Players
                                            select player.Name).ToList());
        }

        #region Network Connection

        private void Agent_OnConnected(object sender, ConnectionStatusEventArgs e)
        {

        }

        private void Agent_OnDisconnecting(object sender, ConnectionStatusEventArgs e)
        {

        }

        private void Agent_OnDisconnected(object sender, ConnectionStatusEventArgs e)
        {

        }

        #endregion
    }
}
