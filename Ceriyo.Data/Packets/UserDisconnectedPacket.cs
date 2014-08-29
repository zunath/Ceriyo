using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProtoBuf;

namespace Ceriyo.Data.Packets
{
    [ProtoContract]
    public class UserDisconnectedPacket: PacketBase
    {
        public UserDisconnectedPacket()
        {
        }
    }
}
