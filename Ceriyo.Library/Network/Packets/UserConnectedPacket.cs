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
