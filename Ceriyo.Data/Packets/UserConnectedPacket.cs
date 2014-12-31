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
    }
}
