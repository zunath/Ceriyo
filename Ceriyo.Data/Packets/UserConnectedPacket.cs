using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ceriyo.Data.GameObjects;
using ProtoBuf;
using Ceriyo.Data.NetworkObjects;

namespace Ceriyo.Data.Packets
{
    [ProtoContract]
    public class UserConnectedPacket : PacketBase
    {
        [ProtoMember(1)]
        public List<PlayerNO> CharacterList { get; set; }
        [ProtoMember(2)]
        public string Announcement { get; set; }
        [ProtoMember(3)]
        public bool CanDeleteCharacters { get; set; }

        public UserConnectedPacket()
        {
            CharacterList = new List<PlayerNO>();
            Announcement = string.Empty;
            CanDeleteCharacters = false;
        }
    }
}
