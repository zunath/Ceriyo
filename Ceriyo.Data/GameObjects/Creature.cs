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
        public SerializableDictionary<ScriptEventType, string> Scripts { get; set; }
        [XmlIgnore]
        public string CategoryName { get { return "Creature"; } }
        public SerializableDictionary<AnimationType, string> AnimationResrefs { get; set; }

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
        public GenderType Gender { get; set; }

        public string DialogResref { get; set; }
        public BindingList<string> AbilityResrefs { get; set; }
        public BindingList<string> SkillResrefs { get; set; }
        public BindingList<string> ItemResrefs { get; set; }
        public SerializableDictionary<InventorySlot, string> EquippedItemResrefs { get; set; }

        public Creature()
        {
            Name = string.Empty;
            Tag = string.Empty;
            Resref = string.Empty;
            Description = string.Empty;
            Comments = string.Empty;
            LocalVariables = new BindingList<LocalVariable>();
            Scripts = new SerializableDictionary<ScriptEventType, string>();
            AnimationResrefs = new SerializableDictionary<AnimationType, string>();

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
            Gender = GenderType.Male;
            AbilityResrefs = new BindingList<string>();
            SkillResrefs = new BindingList<string>();
            ItemResrefs = new BindingList<string>();
            EquippedItemResrefs = new SerializableDictionary<InventorySlot, string>();

            AnimationResrefs.Add(AnimationType.MoveEast, string.Empty);
            AnimationResrefs.Add(AnimationType.MoveNorth, string.Empty);
            AnimationResrefs.Add(AnimationType.MoveNorthEast, string.Empty);
            AnimationResrefs.Add(AnimationType.MoveNorthWest, string.Empty);
            AnimationResrefs.Add(AnimationType.MoveSouth, string.Empty);
            AnimationResrefs.Add(AnimationType.MoveSouthEast, string.Empty);
            AnimationResrefs.Add(AnimationType.MoveSouthWest, string.Empty);
            AnimationResrefs.Add(AnimationType.MoveWest, string.Empty);
            AnimationResrefs.Add(AnimationType.IdleEast, string.Empty);
            AnimationResrefs.Add(AnimationType.IdleNorth, string.Empty);
            AnimationResrefs.Add(AnimationType.IdleNorthEast, string.Empty);
            AnimationResrefs.Add(AnimationType.IdleNorthWest, string.Empty);
            AnimationResrefs.Add(AnimationType.IdleSouth, string.Empty);
            AnimationResrefs.Add(AnimationType.IdleSouthEast, string.Empty);
            AnimationResrefs.Add(AnimationType.IdleSouthWest, string.Empty);
            AnimationResrefs.Add(AnimationType.IdleWest, string.Empty);

            Scripts.Add(ScriptEventType.OnCreatureConversation, string.Empty);
            Scripts.Add(ScriptEventType.OnCreatureAttacked, string.Empty);
            Scripts.Add(ScriptEventType.OnCreatureDamaged, string.Empty);
            Scripts.Add(ScriptEventType.OnCreatureDeath, string.Empty);
            Scripts.Add(ScriptEventType.OnCreatureDisturbed, string.Empty);
            Scripts.Add(ScriptEventType.OnCreatureHeartbeat, string.Empty);
            Scripts.Add(ScriptEventType.OnCreatureSpawned, string.Empty);

            EquippedItemResrefs.Add(InventorySlot.Ammo, string.Empty);
            EquippedItemResrefs.Add(InventorySlot.Arms, string.Empty);
            EquippedItemResrefs.Add(InventorySlot.Back, string.Empty);
            EquippedItemResrefs.Add(InventorySlot.Body, string.Empty);
            EquippedItemResrefs.Add(InventorySlot.Head, string.Empty);
            EquippedItemResrefs.Add(InventorySlot.MainHand, string.Empty);
            EquippedItemResrefs.Add(InventorySlot.Neck, string.Empty);
            EquippedItemResrefs.Add(InventorySlot.OffHand, string.Empty);
            EquippedItemResrefs.Add(InventorySlot.Ring1, string.Empty);
            EquippedItemResrefs.Add(InventorySlot.Ring2, string.Empty);
            EquippedItemResrefs.Add(InventorySlot.Waist, string.Empty);
        }
    }
}
