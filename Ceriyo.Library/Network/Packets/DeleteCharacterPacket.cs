using Ceriyo.Data.Engine;
using Lidgren.Network;
using ProtoBuf;

namespace Ceriyo.Library.Network.Packets
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

        // Receiving from client
        public override NetworkTransferData ServerReceive(NetworkTransferData data)
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

            response.Send(NetDeliveryMethod.ReliableUnordered, SenderConnection);

            return data;
        }

        public override NetworkTransferData ClientReceive(NetworkTransferData data)
        {
            return data;
        }
    }
}
