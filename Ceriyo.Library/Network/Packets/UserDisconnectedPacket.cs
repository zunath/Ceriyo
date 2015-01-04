using ProtoBuf;

namespace Ceriyo.Library.Network.Packets
{
    [ProtoContract]
    public class UserDisconnectedPacket: PacketBase
    {
        public UserDisconnectedPacket()
        {
        }

        public override NetworkTransferData ServerReceive(NetworkTransferData data)
        {
            return data;
        }

        public override NetworkTransferData ClientReceive(NetworkTransferData data)
        {
            return data;
        }
    }
}
