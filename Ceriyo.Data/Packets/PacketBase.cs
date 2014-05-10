using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lidgren.Network;
using ProtoBuf;

namespace Ceriyo.Data.Packets
{
    [ProtoContract]
    public class PacketBase
    {
        public NetConnection SenderConnection { get; set; }

        public PacketBase()
        {
        }

    }
}
