using System;
using Ceriyo.Data.Enumerations;
using Lidgren.Network;
using ProtoBuf;

namespace Ceriyo.Library.Network.Packets
{
    [ProtoContract]
    [ProtoInclude(100, typeof(UserConnectedPacket))]
    [ProtoInclude(101, typeof(UserDisconnectedPacket))]
    [ProtoInclude(102, typeof(UserInfoPacket))]
    [ProtoInclude(103, typeof(DeleteCharacterPacket))]
    [ProtoInclude(104, typeof(CharacterCreationScreenPacket))]
    [ProtoInclude(105, typeof(CreateCharacterPacket))]
    [ProtoInclude(106, typeof(CharacterSelectionScreenPacket))]
    [ProtoInclude(107, typeof(SelectCharacterPacket))]
    [ProtoInclude(108, typeof(GameScreenInitPacket))]
    public abstract class PacketBase
    {
        public NetConnection SenderConnection { get; set; }

        public void Send(NetDeliveryMethod deliveryMethod, NetConnection connection = null)
        {
            if (NetworkManager.GetNetworkRole() == NetworkAgentRoleEnum.Server && connection == null)
            {
                throw new Exception("A NetConnection must be specified when packets are sent from the server.");
            }

            if (connection == null && NetworkManager.GetNetworkRole() == NetworkAgentRoleEnum.Client)
            {
                connection = NetworkManager.GetConnectionToServer();
            }

            NetworkManager.SendPacket(this, connection, deliveryMethod);
        }
    }
}
