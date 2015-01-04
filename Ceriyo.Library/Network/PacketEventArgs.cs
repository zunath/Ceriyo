using System;
using Ceriyo.Library.Network.Packets;

namespace Ceriyo.Library.Network
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
