using ProtoBuf;

namespace Ceriyo.Library.Network.Packets
{
    [ProtoContract]
    public class UserDisconnectedPacket: PacketBase
    {
        public UserDisconnectedPacket()
        {
        }

        public override NetworkTransferData Receive(NetworkTransferData data)
        {
            return data;
        }

    }
}
