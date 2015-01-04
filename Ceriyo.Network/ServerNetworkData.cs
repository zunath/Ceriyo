using System.Collections.Generic;
using Ceriyo.Data.Packets;
using Ceriyo.Data.Server;
using Ceriyo.Data.Settings;
using Lidgren.Network;

namespace Ceriyo.Network
{
    public class ServerNetworkData
    {
        public Dictionary<NetConnection, ServerPlayer> Players { get; set; }
        public ServerSettings Settings { get; set; }
        public PacketBase ResponsePacket { get; set; }
        public NetDeliveryMethod DeliveryMethod { get; set; }


        public ServerNetworkData()
        {
            DeliveryMethod = NetDeliveryMethod.Unreliable;
        }
    }
}
