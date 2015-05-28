using System.ComponentModel;
using System.Xml.Serialization;
using Ceriyo.Data.Engine;
using Ceriyo.Data.Enumerations;
using Ceriyo.Data.Extensions;
using ProtoBuf;

namespace Ceriyo.Data.GameObjects
{
    [ProtoContract]
    public class Creature: IGameObject
    {
        [ProtoMember(1)]
        public string Name { get; set; }
        [ProtoMember(2)]
        public string Tag { get; set; }
        [ProtoMember(3)]
        public string Resref { get; set; }
        [ProtoMember(4)]
        public string Description { get; set; }
        public string Comments { get; set; }
        [XmlIgnore]
        public string WorkingDirectory { get { return WorkingPaths.CreaturesDirectory; } }
        public BindingList<LocalVariable> LocalVariables { get; set; }
        public SerializableDictionary<ScriptEventTypeEnum, string> Scripts { get; set; }
        [XmlIgnore]
        public string CategoryName { get { return "Creature"; } }
        public SerializableDictionary<AnimationTypeEnum, string> AnimationResrefs { get; set; }

        public string CharacterClassResref { get; set; }
        public int Level { get; set; }
        public int HitPoints { get; set; }
        public int Mana { get; set; }
        public int Strength { get; set; }
        public int Dexterity { get; set; }
        public int Constitution { get; set; }
        public int Wisdom { get; set; }
        public int Intelligence { get; set; }
        public int Charisma { get; set; }
        public GenderTypeEnum Gender { get; set; }

        public string DialogResref { get; set; }
        public BindingList<string> AbilityResrefs { get; set; }
        public BindingList<string> SkillResrefs { get; set; }
        public BindingList<string> ItemResrefs { get; set; }
        public SerializableDictionary<InventorySlotEnum, string> EquippedItemResrefs { get; set; }

        public Creature()
        {
            Name = string.Empty;
            Tag = string.Empty;
            Resref = string.Empty;
            Description = string.Empty;
            Comments = string.Empty;
            LocalVariables = new BindingList<LocalVariable>();
            Scripts = new SerializableDictionary<ScriptEventTypeEnum, string>();
            AnimationResrefs = new SerializableDictionary<AnimationTypeEnum, string>();

            CharacterClassResref = string.Empty;
            Level = 1;
            HitPoints = 0;
            Strength = 0;
            Dexterity = 0;
            Constitution = 0;
            Wisdom = 0;
            Intelligence = 0;
            Charisma = 0;

            DialogResref = string.Empty;
            Gender = GenderTypeEnum.Male;
            AbilityResrefs = new BindingList<string>();
            SkillResrefs = new BindingList<string>();
            ItemResrefs = new BindingList<string>();
            EquippedItemResrefs = new SerializableDictionary<InventorySlotEnum, string>();

            AnimationResrefs.Add(AnimationTypeEnum.MoveEast, string.Empty);
            AnimationResrefs.Add(AnimationTypeEnum.MoveNorth, string.Empty);
            AnimationResrefs.Add(AnimationTypeEnum.MoveNorthEast, string.Empty);
            AnimationResrefs.Add(AnimationTypeEnum.MoveNorthWest, string.Empty);
            AnimationResrefs.Add(AnimationTypeEnum.MoveSouth, string.Empty);
            AnimationResrefs.Add(AnimationTypeEnum.MoveSouthEast, string.Empty);
            AnimationResrefs.Add(AnimationTypeEnum.MoveSouthWest, string.Empty);
            AnimationResrefs.Add(AnimationTypeEnum.MoveWest, string.Empty);
            AnimationResrefs.Add(AnimationTypeEnum.IdleEast, string.Empty);
            AnimationResrefs.Add(AnimationTypeEnum.IdleNorth, string.Empty);
            AnimationResrefs.Add(AnimationTypeEnum.IdleNorthEast, string.Empty);
            AnimationResrefs.Add(AnimationTypeEnum.IdleNorthWest, string.Empty);
            AnimationResrefs.Add(AnimationTypeEnum.IdleSouth, string.Empty);
            AnimationResrefs.Add(AnimationTypeEnum.IdleSouthEast, string.Empty);
            AnimationResrefs.Add(AnimationTypeEnum.IdleSouthWest, string.Empty);
            AnimationResrefs.Add(AnimationTypeEnum.IdleWest, string.Empty);

            Scripts.Add(ScriptEventTypeEnum.OnCreatureConversation, string.Empty);
            Scripts.Add(ScriptEventTypeEnum.OnCreatureAttacked, string.Empty);
            Scripts.Add(ScriptEventTypeEnum.OnCreatureDamaged, string.Empty);
            Scripts.Add(ScriptEventTypeEnum.OnCreatureDeath, string.Empty);
            Scripts.Add(ScriptEventTypeEnum.OnCreatureDisturbed, string.Empty);
            Scripts.Add(ScriptEventTypeEnum.OnCreatureHeartbeat, string.Empty);
            Scripts.Add(ScriptEventTypeEnum.OnCreatureSpawned, string.Empty);

            EquippedItemResrefs.Add(InventorySlotEnum.Ammo, string.Empty);
            EquippedItemResrefs.Add(InventorySlotEnum.Arms, string.Empty);
            EquippedItemResrefs.Add(InventorySlotEnum.Back, string.Empty);
            EquippedItemResrefs.Add(InventorySlotEnum.Body, string.Empty);
            EquippedItemResrefs.Add(InventorySlotEnum.Head, string.Empty);
            EquippedItemResrefs.Add(InventorySlotEnum.MainHand, string.Empty);
            EquippedItemResrefs.Add(InventorySlotEnum.Neck, string.Empty);
            EquippedItemResrefs.Add(InventorySlotEnum.OffHand, string.Empty);
            EquippedItemResrefs.Add(InventorySlotEnum.Ring1, string.Empty);
            EquippedItemResrefs.Add(InventorySlotEnum.Ring2, string.Empty);
            EquippedItemResrefs.Add(InventorySlotEnum.Waist, string.Empty);
        }
    }
}
