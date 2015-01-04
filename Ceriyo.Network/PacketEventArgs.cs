using System;
using Ceriyo.Data.Packets;

namespace Ceriyo.Network
{
    public class PacketEventArgs : EventArgs
    {
        public PacketBase Packet { get; set; }

        public PacketEventArgs(PacketBase packet)
        {
            Packet = packet;
        }
    }
}
