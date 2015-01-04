using ProtoBuf;

namespace Ceriyo.Network.Packets
{
    [ProtoContract]
    public class UserDisconnectedPacket: PacketBase
    {
        public UserDisconnectedPacket()
        {
        }

        public override ServerNetworkData Receive(ServerNetworkData data)
        {
            return data;
        }

        public override ServerNetworkData Send(ServerNetworkData data)
        {
            return data;
        }
    }
}
