using ProtoBuf;

namespace Ceriyo.Infrastructure.Network.Packets.CharacterManagement
{
    [ProtoContract]
    public class DeleteCharacterPacket: PacketBase
    {
        [ProtoMember(1)]
        public string PCGlobalID { get; set; }

        public override void Process()
        {
            
        }
    }
}
