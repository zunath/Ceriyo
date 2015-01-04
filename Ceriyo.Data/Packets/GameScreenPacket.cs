using Ceriyo.Data.GameObjects;
using Ceriyo.Data.Server;
using Lidgren.Network;
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

        public override ServerGameData Receive(ServerGameData data)
        {
            GameScreenPacket response = new GameScreenPacket
            {
                PC = data.Players[SenderConnection].PC
            };

            data.ResponsePacket = response;
            data.DeliveryMethod = NetDeliveryMethod.ReliableUnordered;

            return data;
        }

        public override ServerGameData Send(ServerGameData data)
        {
            return data;
        }
    }
}
