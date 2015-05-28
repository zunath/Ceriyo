using Ceriyo.Data.GameObjects;
using ProtoBuf;

namespace Ceriyo.Library.Network.Packets
{
    [ProtoContract]
    public class CreateCharacterPacket: PacketBase
    {
        [ProtoMember(1)]
        public string Name { get; set; }
        [ProtoMember(2)]
        public string Description { get; set; }
        [ProtoMember(3)]
        public Player ResponsePlayer { get; set; }

        public CreateCharacterPacket()
        {
            Name = string.Empty;
            Description = string.Empty;
            ResponsePlayer = new Player();
        }

    }
}
