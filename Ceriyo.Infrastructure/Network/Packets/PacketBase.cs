using Ceriyo.Infrastructure.Network.Contracts;
using Ceriyo.Infrastructure.Network.Packets.CharacterManagement;
using Ceriyo.Infrastructure.Network.Packets.Connection;
using ProtoBuf;

namespace Ceriyo.Infrastructure.Network.Packets
{
    [ProtoContract]
    [ProtoInclude(1, typeof(ConnectionRequestPacket))]
    [ProtoInclude(2, typeof(ConnectedToServerPacket))]
    [ProtoInclude(3, typeof(CharacterCreatedPacket))]
    [ProtoInclude(4, typeof(CreateCharacterPacket))]
    [ProtoInclude(5, typeof(CharacterSelectedPacket))]
    [ProtoInclude(6, typeof(CharacterAddedToWorldPacket))]
    public abstract class PacketBase : INetworkPacket
    {
        public abstract void Process();

    }
}
