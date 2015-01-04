﻿using Ceriyo.Data.Engine;
using Ceriyo.Data.GameObjects;
using Ceriyo.Data.Server;
using Lidgren.Network;
using ProtoBuf;

namespace Ceriyo.Data.Packets
{
    [ProtoContract]
    public class CreateCharacterPacket: PacketBase
    {
        [ProtoMember(1)]
        public string Name { get; set; }
        [ProtoMember(2)]
        public string Description { get; set; }
        [ProtoMember(3)]
        public Player ResponsePlayer { get; set; }

        public CreateCharacterPacket()
        {
            Name = string.Empty;
            Description = string.Empty;
            ResponsePlayer = new Player();
        }

        public override ServerGameData Receive(ServerGameData data)
        {
            Player pc = new Player
            {
                Name = Name,
                Description = Description
            };

            string username = data.Players[SenderConnection].Username;
            EngineDataManager.SavePlayer(username, pc, true);

            CreateCharacterPacket response = new CreateCharacterPacket
            {
                ResponsePlayer = pc
            };

            data.ResponsePacket = response;
            data.DeliveryMethod = NetDeliveryMethod.ReliableUnordered;

            return data;
        }

        public override ServerGameData Send(ServerGameData data)
        {
            return data;
        }
    }
}
