using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ceriyo.Data.GameObjects;
using ProtoBuf;

namespace Ceriyo.Data.Packets
{
    [ProtoContract]
    public class UserConnectedPacket : PacketBase
    {
        [ProtoMember(1)]
        public List<Player> CharacterList { get; set; }
        [ProtoMember(2)]
        public string Announcement { get; set; }
        [ProtoMember(3)]
        public bool CanDeleteCharacters { get; set; }

        public UserConnectedPacket()
        {
            CharacterList = new List<Player>();
            Announcement = string.Empty;
            CanDeleteCharacters = false;
        }
    }
}
