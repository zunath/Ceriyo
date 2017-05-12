using ProtoBuf;

namespace Ceriyo.Infrastructure.Network.Packets
{
    [ProtoContract]
    public class ConnectionRequestPacket: PacketBase
    {
        [ProtoMember(1)]
        public string Username { get; set; }
        [ProtoMember(2)]
        public string Password { get; set; }
        public override void Process()
        {
            
        }
    }
}
