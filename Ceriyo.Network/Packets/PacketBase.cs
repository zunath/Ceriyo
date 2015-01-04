using Lidgren.Network;
using ProtoBuf;

namespace Ceriyo.Network.Packets
{
    [ProtoContract]
    [ProtoInclude(100, typeof(UserConnectedPacket))]
    [ProtoInclude(101, typeof(UserDisconnectedPacket))]
    [ProtoInclude(102, typeof(UserInfoPacket))]
    [ProtoInclude(103, typeof(DeleteCharacterPacket))]
    [ProtoInclude(104, typeof(CharacterCreationScreenPacket))]
    [ProtoInclude(105, typeof(CreateCharacterPacket))]
    [ProtoInclude(106, typeof(CharacterSelectionScreenPacket))]
    [ProtoInclude(107, typeof(GameScreenPacket))]
    [ProtoInclude(108, typeof(SelectCharacterPacket))]
    [ProtoInclude(109, typeof(EnteringGameScreenPacket))]
    public abstract class PacketBase
    {
        public NetConnection SenderConnection { get; set; }

        public PacketBase()
        {
        }

        public abstract ServerNetworkData Receive(ServerNetworkData data);
        public abstract ServerNetworkData Send(ServerNetworkData data);
    }
}
