using Ceriyo.Core.Contracts;
using Ceriyo.Infrastructure.Network.Contracts;
using ProtoBuf;

namespace Ceriyo.Infrastructure.Network.Packets
{
    [ProtoContract]
    [ProtoInclude(1, typeof(ConnectionRequestPacket))]
    [ProtoInclude(2, typeof(ConnectedToServerPacket))]
    public abstract class PacketBase : INetworkPacket
    {
        public abstract void Process();

    }
}
