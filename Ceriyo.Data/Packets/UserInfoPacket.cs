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
        public bool IsRequest { get; set; }
        public string Username { get; set; }

        public UserInfoPacket()
        {
            IsRequest = false;
            Username = string.Empty;
        }
    }
}
