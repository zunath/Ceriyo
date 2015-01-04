using System.Collections.Generic;
using System.Linq;
using Ceriyo.Data.Engine;
using Ceriyo.Data.GameObjects;
using Ceriyo.Data.Server;
using Ceriyo.Network;
using Lidgren.Network;
using ProtoBuf;

namespace Ceriyo.Data.Packets
{
    [ProtoContract]
    public class CharacterCreationScreenPacket : PacketBase
    {
        [ProtoMember(1)]
        public bool IsRequest { get; set; }
        [ProtoMember(3)]
        public List<CharacterClass> CharacterClasses { get; set; }
        [ProtoMember(4)]
        public List<Ability> Abilities { get; set; }
        [ProtoMember(5)]
        public List<Skill> Skills { get; set; }

        public CharacterCreationScreenPacket()
        {
            IsRequest = false;
            CharacterClasses = new List<CharacterClass>();
            Abilities = new List<Ability>();
            Skills = new List<Skill>();
        }

        public override ServerNetworkData Receive(ServerNetworkData data)
        {
            CharacterCreationScreenPacket response = new CharacterCreationScreenPacket
            {
                Abilities = WorkingDataManager.GetAllGameObjects<Ability>(ModulePaths.AbilitiesDirectory).ToList(),
                CharacterClasses = WorkingDataManager.GetAllGameObjects<CharacterClass>(ModulePaths.CharacterClassesDirectory).ToList(),
                Skills = WorkingDataManager.GetAllGameObjects<Skill>(ModulePaths.SkillsDirectory).ToList()
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
