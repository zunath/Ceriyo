using System.Collections.Generic;
using System.Linq;
using Ceriyo.Data.Engine;
using Ceriyo.Data.GameObjects;
using Lidgren.Network;
using ProtoBuf;

namespace Ceriyo.Library.Network.Packets
{
    [ProtoContract]
    public class CharacterCreationScreenPacket : PacketBase
    {
        [ProtoMember(1)]
        public bool IsRequest { get; set; }
        [ProtoMember(2)]
        public List<CharacterClass> CharacterClasses { get; set; }
        [ProtoMember(3)]
        public List<Ability> Abilities { get; set; }
        [ProtoMember(4)]
        public List<Skill> Skills { get; set; }

        public CharacterCreationScreenPacket()
        {
            IsRequest = false;
            CharacterClasses = new List<CharacterClass>();
            Abilities = new List<Ability>();
            Skills = new List<Skill>();
        }

        // Receiving from client
        public override NetworkTransferData ServerReceive(NetworkTransferData data)
        {
            CharacterCreationScreenPacket response = new CharacterCreationScreenPacket
            {
                Abilities = WorkingDataManager.GetAllGameObjects<Ability>(ModulePaths.AbilitiesDirectory).ToList(),
                CharacterClasses = WorkingDataManager.GetAllGameObjects<CharacterClass>(ModulePaths.CharacterClassesDirectory).ToList(),
                Skills = WorkingDataManager.GetAllGameObjects<Skill>(ModulePaths.SkillsDirectory).ToList()
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
