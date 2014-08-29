using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ceriyo.Data.Packets;

namespace Ceriyo.Data.EventArguments
{
    public class PacketEventArgs : EventArgs
    {
        public PacketBase Packet { get; set; }

        public PacketEventArgs(PacketBase packet)
        {
            this.Packet = packet;
        }
    }
}
