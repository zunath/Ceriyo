using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProtoBuf;

namespace Ceriyo.Data.Packets
{
    [ProtoContract]
    public class UserInfoPacket : PacketBase
    {
        [ProtoMember(1)]
        public bool IsRequest { get; set; }
        [ProtoMember(2)]
        public string Username { get; set; }

        public UserInfoPacket()
        {
            this.IsRequest = false;
            this.Username = string.Empty;
        }
    }
}
