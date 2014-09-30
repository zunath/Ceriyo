using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            this.IsRequest = false;
            this.Races = new List<Race>();
            this.CharacterClasses = new List<CharacterClass>();
            this.Abilities = new List<Ability>();
            this.Skills = new List<Skill>();
        }
    }
}
