using Ceriyo.Data.Server;
using Ceriyo.Network;
using ProtoBuf;

namespace Ceriyo.Data.Packets
{
    [ProtoContract]
    public class UserConnectedPacket : PacketBase
    {
        public bool IsSuccessful { get; set; }
        public string ErrorMessage { get; set; }

        public UserConnectedPacket()
        {
            IsSuccessful = false;
            ErrorMessage = string.Empty;
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
