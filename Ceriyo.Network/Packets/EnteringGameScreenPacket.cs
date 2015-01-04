﻿using System.ComponentModel;
using Ceriyo.Data.GameObjects;
using Lidgren.Network;
using ProtoBuf;

namespace Ceriyo.Network.Packets
{
    [ProtoContract]
    public class EnteringGameScreenPacket : PacketBase
    {
        [ProtoMember(1)]
        public bool IsRequest { get; set; }
        [ProtoMember(2)]
        public string AreaName { get; set; }
        [ProtoMember(3)]
        public string AreaTag { get; set; }
        [ProtoMember(4)]
        public string AreaResref { get; set; }
        [ProtoMember(5)]
        public string AreaDescription { get; set; }

        [ProtoMember(6)]
        public BindingList<MapTile> AreaTiles { get; set; } 


        public EnteringGameScreenPacket()
        {
            IsRequest = false;
            AreaName = string.Empty;
            AreaTag = string.Empty;
            AreaResref = string.Empty;
            AreaDescription = string.Empty;

            AreaTiles = new BindingList<MapTile>();
        }

        public override ServerNetworkData Receive(ServerNetworkData data)
        {
            EnteringGameScreenPacket response = new EnteringGameScreenPacket
            {

            };

            data.ResponsePacket = response;
            data.DeliveryMethod = NetDeliveryMethod.ReliableUnordered;

            return data;
        }

        public override ServerNetworkData Send(ServerNetworkData data)
        {
            return data;
        }
    }
}
