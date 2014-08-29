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
        public List<PlayerNO> PlayerList { get; set; }
        [ProtoMember(2)]
        public string Announcement { get; set; }

        public UserConnectedPacket()
        {
            PlayerList = new List<PlayerNO>();
            Announcement = string.Empty;
        }
    }
}
