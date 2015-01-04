using ProtoBuf;

namespace Ceriyo.Library.Network.Packets
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

        public override NetworkTransferData Receive(NetworkTransferData data)
        {
            return data;
        }

    }
}
