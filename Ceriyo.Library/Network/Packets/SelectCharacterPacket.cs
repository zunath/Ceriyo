using Ceriyo.Data.Engine;
using Ceriyo.Data.GameObjects;
using Lidgren.Network;
using ProtoBuf;

namespace Ceriyo.Library.Network.Packets
{
    [ProtoContract]
    public class SelectCharacterPacket : PacketBase
    {
        [ProtoMember(1)]
        public string Resref { get; set; }
        [ProtoMember(2)]
        public bool IsSuccessful { get; set; }

        public SelectCharacterPacket()
        {
            Resref = string.Empty;
            IsSuccessful = false;
        }

        public override NetworkTransferData Receive(NetworkTransferData data)
        {
            string username = data.Players[SenderConnection].Username;
            Player pc = EngineDataManager.GetPlayer(username, Resref);

            SelectCharacterPacket response = new SelectCharacterPacket();

            if (pc != null)
            {
                data.Players[SenderConnection].PC = pc;
                response.IsSuccessful = true;
            }

            response.Send(NetDeliveryMethod.ReliableUnordered, SenderConnection);
            
            return data;
        }

    }
}
