using Ceriyo.Core.Constants;
using ProtoBuf;

namespace Ceriyo.Infrastructure.Network.Packets.CharacterManagement
{
    [ProtoContract]
    public class CharacterDeletedPacket: PacketBase
    {
        [ProtoMember(1)]
        public string PCGlobalID { get; set; }
        [ProtoMember(2)]
        public DeleteCharacterFailureType FailureType { get; set; }

        public override void Process()
        {
            
        }
    }
}
