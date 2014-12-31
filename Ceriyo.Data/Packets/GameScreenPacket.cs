using Ceriyo.Data.GameObjects;
using ProtoBuf;

namespace Ceriyo.Data.Packets
{
    [ProtoContract]
    public class GameScreenPacket : PacketBase
    {
        [ProtoMember(1)]
        public bool IsRequest { get; set; }
        [ProtoMember(2)]
        public Player PC { get; set; }

        public GameScreenPacket()
        {
            IsRequest = false;
            PC = new Player();
        }
    }
}
