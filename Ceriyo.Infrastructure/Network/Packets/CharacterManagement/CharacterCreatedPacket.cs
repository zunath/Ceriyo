using ProtoBuf;

namespace Ceriyo.Infrastructure.Network.Packets.CharacterManagement
{
    [ProtoContract]
    public class CharacterCreatedPacket: PacketBase
    {
        [ProtoMember(1)]
        public string GlobalID { get; set; }
        [ProtoMember(2)]
        public string FirstName { get; set; }
        [ProtoMember(3)]
        public string LastName { get; set; }
        [ProtoMember(4)]
        public string Description { get; set; }
        [ProtoMember(5)]
        public int Level { get; set; }

        public override void Process()
        {
            
        }
    }
}
