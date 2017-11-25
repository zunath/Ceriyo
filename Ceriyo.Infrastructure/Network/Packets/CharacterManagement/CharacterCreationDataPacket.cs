using System.Collections.Generic;
using Ceriyo.Core.Data;
using ProtoBuf;

namespace Ceriyo.Infrastructure.Network.Packets.CharacterManagement
{
    [ProtoContract]
    public class CharacterCreationDataPacket: PacketBase
    {
        [ProtoMember(1)]
        public List<ClassData> Classes { get; set; }

        [ProtoMember(2)]
        public bool IsRequest { get; set; }

        public override void Process()
        {
            
        }
    }
}
