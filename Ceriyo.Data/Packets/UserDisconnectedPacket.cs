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
