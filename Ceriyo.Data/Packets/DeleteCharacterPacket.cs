using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProtoBuf;

namespace Ceriyo.Data.Packets
{
    [ProtoContract]
    public class DeleteCharacterPacket : PacketBase
    {
        public bool IsRequest { get; set; }
        public bool IsDeleteSuccessful { get; set; }
        public string CharacterResref { get; set; }

        public DeleteCharacterPacket()
        {
            IsRequest = false;
            IsDeleteSuccessful = false;
            CharacterResref = string.Empty;
        }
    }
}
