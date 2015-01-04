using Ceriyo.Data.Server;
using ProtoBuf;

namespace Ceriyo.Data.Packets
{
    [ProtoContract]
    public class UserDisconnectedPacket: PacketBase
    {
        public UserDisconnectedPacket()
        {
        }

        public override ServerGameData Receive(ServerGameData data)
        {
            return data;
        }

        public override ServerGameData Send(ServerGameData data)
        {
            return data;
        }
    }
}
