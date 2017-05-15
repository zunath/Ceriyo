using ProtoBuf;

namespace Ceriyo.Infrastructure.Network.Packets.CharacterManagement
{
    [ProtoContract]
    public class CreateCharacterPacket: PacketBase
    {
        [ProtoMember(1)]
        public string FirstName { get; set; }
        [ProtoMember(2)]
        public string LastName { get; set; }

        public override void Process()
        {
            
        }
    }
}
