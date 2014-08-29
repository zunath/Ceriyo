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
        public List<Player> PlayerList { get; set; }
        public string Announcement { get; set; }

        public UserConnectedPacket()
        {
            PlayerList = new List<Player>();
            Announcement = string.Empty;
        }
    }
}
