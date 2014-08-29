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
        public List<string> PlayerList { get; set; }
        [ProtoMember(2)]
        public string Announcement { get; set; }

        public UserConnectedPacket()
        {
            PlayerList = new List<string>(); // TODO: Set up DTO for sending character lists
            Announcement = string.Empty;
        }
    }
}
