using System.Collections.Generic;
using Ceriyo.Data.Engine;
using Ceriyo.Data.GameObjects;
using Lidgren.Network;
using ProtoBuf;

namespace Ceriyo.Library.Network.Packets
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

        public override NetworkTransferData Receive(NetworkTransferData data)
        {
            string username = data.Players[SenderConnection].Username;
            List<Player> characters = EngineDataManager.GetPlayers(username);

            CharacterSelectionScreenPacket response = new CharacterSelectionScreenPacket
            {
                CharacterList = characters,
                Announcement = data.Settings.Announcement,
                CanDeleteCharacters = data.Settings.AllowCharacterDeletion
            };

            response.Send(NetDeliveryMethod.ReliableUnordered, SenderConnection);
            
            return data;
            
        }

    }
}
