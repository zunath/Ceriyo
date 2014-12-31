using ProtoBuf;

namespace Ceriyo.Data.Packets
{
    [ProtoContract]
    public class UserInfoPacket : PacketBase
    {
        [ProtoMember(1)]
        public bool IsRequest { get; set; }
        [ProtoMember(2)]
        public string Username { get; set; }
        [ProtoMember(3)]
        public string ServerPassword { get; set; }

        public UserInfoPacket()
        {
            IsRequest = false;
            Username = string.Empty;
            ServerPassword = string.Empty;
        }
    }
}
