
using System.Collections.Generic;
using Ceriyo.Data.Packets;
using Ceriyo.Data.Settings;
using Lidgren.Network;

namespace Ceriyo.Data.Server
{
    public class ServerGameData
    {
        public Dictionary<NetConnection, ServerPlayer> Players { get; set; }
        public ServerSettings Settings { get; set; }
        public PacketBase ResponsePacket { get; set; }
        public NetDeliveryMethod DeliveryMethod { get; set; }


        public ServerGameData()
        {
            DeliveryMethod = NetDeliveryMethod.Unreliable;
        }
    }
}
