using System.Collections.Generic;
using Ceriyo.Data.GameObjects;
using ProtoBuf;

namespace Ceriyo.Data.Packets
{
    [ProtoContract]
    public class CharacterCreationScreenPacket : PacketBase
    {
        [ProtoMember(1)]
        public bool IsRequest { get; set; }
        [ProtoMember(2)]
        public List<Race> Races { get; set; }
        [ProtoMember(3)]
        public List<CharacterClass> CharacterClasses { get; set; }
        [ProtoMember(4)]
        public List<Ability> Abilities { get; set; }
        [ProtoMember(5)]
        public List<Skill> Skills { get; set; }

        public CharacterCreationScreenPacket()
        {
            IsRequest = false;
            Races = new List<Race>();
            CharacterClasses = new List<CharacterClass>();
            Abilities = new List<Ability>();
            Skills = new List<Skill>();
        }
    }
}
