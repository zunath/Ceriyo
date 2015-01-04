using Ceriyo.Data.GameObjects;
using Lidgren.Network;
using ProtoBuf;

namespace Ceriyo.Library.Network.Packets
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

        public override NetworkTransferData Receive(NetworkTransferData data)
        {
            GameScreenPacket response = new GameScreenPacket
            {
                PC = data.Players[SenderConnection].PC
            };

            response.Send(NetDeliveryMethod.ReliableUnordered, SenderConnection);

            return data;
        }

    }
}
