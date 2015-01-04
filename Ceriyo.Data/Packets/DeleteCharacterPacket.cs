using Ceriyo.Data.Engine;
using Ceriyo.Data.Server;
using Lidgren.Network;
using ProtoBuf;

namespace Ceriyo.Data.Packets
{
    [ProtoContract]
    public class DeleteCharacterPacket : PacketBase
    {
        [ProtoMember(1)]
        public bool IsRequest { get; set; }
        [ProtoMember(2)]
        public bool IsDeleteSuccessful { get; set; }
        [ProtoMember(3)]
        public string CharacterResref { get; set; }

        public DeleteCharacterPacket()
        {
            IsRequest = false;
            IsDeleteSuccessful = false;
            CharacterResref = string.Empty;
        }

        public override ServerGameData Receive(ServerGameData data)
        {
            bool success = false;

            if (data.Settings.AllowCharacterDeletion)
            {
                success = EngineDataManager.DeletePlayer(data.Players[SenderConnection].Username, CharacterResref);
            }

            DeleteCharacterPacket response = new DeleteCharacterPacket
            {
                IsDeleteSuccessful = success
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
