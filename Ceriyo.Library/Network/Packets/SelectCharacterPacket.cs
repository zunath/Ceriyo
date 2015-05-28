using ProtoBuf;

namespace Ceriyo.Library.Network.Packets
{
    [ProtoContract]
    public class SelectCharacterPacket : PacketBase
    {
        [ProtoMember(1)]
        public string Resref { get; set; }
        [ProtoMember(2)]
        public bool IsSuccessful { get; set; }

        public SelectCharacterPacket()
        {
            Resref = string.Empty;
            IsSuccessful = false;
        }
    }
}
