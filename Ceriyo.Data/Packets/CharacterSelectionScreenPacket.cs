using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ceriyo.Data.GameObjects;
using ProtoBuf;

namespace Ceriyo.Data.Packets
{
    [ProtoContract]
    public class CharacterSelectionScreenPacket : PacketBase
    {
        [ProtoMember(1)]
        public bool IsRequest { get; set; }
        [ProtoMember(2)]
        public List<Player> CharacterList { get; set; }
        [ProtoMember(3)]
        public string Announcement { get; set; }
        [ProtoMember(4)]
        public bool CanDeleteCharacters { get; set; }

        public CharacterSelectionScreenPacket()
        {
            this.IsRequest = false;
            CharacterList = new List<Player>();
            Announcement = string.Empty;
            CanDeleteCharacters = false;
        }
    }
}
