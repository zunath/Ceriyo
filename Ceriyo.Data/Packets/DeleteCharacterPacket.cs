using ProtoBuf;

namespace Ceriyo.Data.Packets
{
    [ProtoContract]
    public class DeleteCharacterPacket : PacketBase
    {
        [ProtoMember(1)]
        public bool IsRequest { get; set; }
        [ProtoMember(2)]
        public bool IsDeleteSuccessful { get; set; }
        [ProtoMember(3)]
        public string CharacterResref { get; set; }

        public DeleteCharacterPacket()
        {
            IsRequest = false;
            IsDeleteSuccessful = false;
            CharacterResref = string.Empty;
        }
    }
}
