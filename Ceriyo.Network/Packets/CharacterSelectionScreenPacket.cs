using System.Collections.Generic;
using Ceriyo.Data.Engine;
using Ceriyo.Data.GameObjects;
using Ceriyo.Data.Server;
using Ceriyo.Network;
using Lidgren.Network;
using ProtoBuf;

namespace Ceriyo.Data.Packets
{
    [ProtoContract]
    public class CharacterSelectionScreenPacket : PacketBase
    {
        [ProtoMember(1)]
        public bool IsRequest { get; set; }
        [ProtoMember(2)]
        public List<Player> CharacterList { get; set; }
        [ProtoMember(3)]
        public string Announcement { get; set; }
        [ProtoMember(4)]
        public bool CanDeleteCharacters { get; set; }

        public CharacterSelectionScreenPacket()
        {
            IsRequest = false;
            CharacterList = new List<Player>();
            Announcement = string.Empty;
            CanDeleteCharacters = false;
        }

        public override ServerNetworkData Receive(ServerNetworkData data)
        {
            string username = data.Players[SenderConnection].Username;
            List<Player> characters = EngineDataManager.GetPlayers(username);

            CharacterSelectionScreenPacket response = new CharacterSelectionScreenPacket
            {
                CharacterList = characters,
                Announcement = data.Settings.Announcement,
                CanDeleteCharacters = data.Settings.AllowCharacterDeletion
            };

            data.ResponsePacket = response;
            data.DeliveryMethod = NetDeliveryMethod.ReliableUnordered;

            return data;
            
        }

        public override ServerNetworkData Send(ServerNetworkData data)
        {
            return data;
            
        }
    }
}
