using Ceriyo.Data.Engine;
using Ceriyo.Data.GameObjects;
using Lidgren.Network;
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

        // Receiving from client
        public override NetworkTransferData ServerReceive(NetworkTransferData data)
        {
            Player pc = new Player
            {
                Name = Name,
                Description = Description
            };

            string username = data.Players[SenderConnection].Username;
            EngineDataManager.SavePlayer(username, pc, true);

            CreateCharacterPacket response = new CreateCharacterPacket
            {
                ResponsePlayer = pc
            };

            response.Send(NetDeliveryMethod.ReliableUnordered, SenderConnection);
            
            return data;
        }

        public override NetworkTransferData ClientReceive(NetworkTransferData data)
        {
            return data;
        }
    }
}
