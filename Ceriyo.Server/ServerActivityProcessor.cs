using Ceriyo.Data.Enumerations;
using Ceriyo.Data.Packets;
using Ceriyo.Library.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ceriyo.Server
{
    public class ServerActivityProcessor
    {
        private NetworkAgent Agent { get; set; }

        public ServerActivityProcessor(int port)
        {
            Agent = new NetworkAgent(NetworkAgentRoleEnum.Server, port);
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
    }
}
