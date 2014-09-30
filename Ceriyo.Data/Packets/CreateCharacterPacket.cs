using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ceriyo.Data.GameObjects;
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
            this.Name = string.Empty;
            this.Description = string.Empty;
            this.ResponsePlayer = new Player();
        }
    }
}
