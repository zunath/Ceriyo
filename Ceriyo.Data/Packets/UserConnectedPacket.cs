using Ceriyo.Data.Server;
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
