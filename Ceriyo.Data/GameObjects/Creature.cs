using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Ceriyo.Data.Enumerations;
using Ceriyo.Data.Extensions;
using ProtoBuf;

namespace Ceriyo.Data.GameObjects
{
    [ProtoContract]
    public class Creature: IGameObject
    {
        [XmlIgnore]
        private WorkingDataManager WorkingManager { get; set; }

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


        [XmlIgnore]
        public Dialog ConversationDialog
        {
            get
            {
                return WorkingManager.GetGameObject<Dialog>(ModulePaths.DialogsDirectory, DialogResref);
            }
        }

        [XmlIgnore]
        public CharacterClass CharClass
        {
            get
            {
                return WorkingManager.GetGameObject<CharacterClass>(ModulePaths.CharacterClassesDirectory, this.CharacterClassResref);
            }
        }

        [XmlIgnore]
        public BindingList<Ability> Abilities
        {
            get
            {
                return new BindingList<Ability>(
                    WorkingManager.GetAllGameObjects<Ability>(ModulePaths.AbilitiesDirectory)
                        .Where(x => AbilityResrefs.Contains(x.Resref))
                        .ToList());
            }
        }

        [XmlIgnore]
        public BindingList<Item> Items
        {
            get
            {
                return new BindingList<Item>(
                    WorkingManager.GetAllGameObjects<Item>(ModulePaths.ItemsDirectory)
                        .Where(x => ItemResrefs.Contains(x.Resref))
                        .ToList());
            }
        }

        public Creature()
        {
            this.WorkingManager = new WorkingDataManager();

            this.Name = string.Empty;
            this.Tag = string.Empty;
            this.Resref = string.Empty;
            this.Description = string.Empty;
            this.Comments = string.Empty;
            this.LocalVariables = new BindingList<LocalVariable>();
            this.Scripts = new SerializableDictionary<ScriptEventTypeEnum, string>();
            this.AnimationResrefs = new SerializableDictionary<AnimationTypeEnum, string>();

            this.CharacterClassResref = string.Empty;
            this.Level = 1;
            this.HitPoints = 0;
            this.Strength = 0;
            this.Dexterity = 0;
            this.Constitution = 0;
            this.Wisdom = 0;
            this.Intelligence = 0;
            this.Charisma = 0;

            this.DialogResref = string.Empty;
            this.Gender = GenderTypeEnum.Male;
            this.AbilityResrefs = new BindingList<string>();
            this.SkillResrefs = new BindingList<string>();
            this.ItemResrefs = new BindingList<string>();
            this.EquippedItemResrefs = new SerializableDictionary<InventorySlotEnum, string>();

            this.AnimationResrefs.Add(AnimationTypeEnum.MoveEast, string.Empty);
            this.AnimationResrefs.Add(AnimationTypeEnum.MoveNorth, string.Empty);
            this.AnimationResrefs.Add(AnimationTypeEnum.MoveNorthEast, string.Empty);
            this.AnimationResrefs.Add(AnimationTypeEnum.MoveNorthWest, string.Empty);
            this.AnimationResrefs.Add(AnimationTypeEnum.MoveSouth, string.Empty);
            this.AnimationResrefs.Add(AnimationTypeEnum.MoveSouthEast, string.Empty);
            this.AnimationResrefs.Add(AnimationTypeEnum.MoveSouthWest, string.Empty);
            this.AnimationResrefs.Add(AnimationTypeEnum.MoveWest, string.Empty);
            this.AnimationResrefs.Add(AnimationTypeEnum.IdleEast, string.Empty);
            this.AnimationResrefs.Add(AnimationTypeEnum.IdleNorth, string.Empty);
            this.AnimationResrefs.Add(AnimationTypeEnum.IdleNorthEast, string.Empty);
            this.AnimationResrefs.Add(AnimationTypeEnum.IdleNorthWest, string.Empty);
            this.AnimationResrefs.Add(AnimationTypeEnum.IdleSouth, string.Empty);
            this.AnimationResrefs.Add(AnimationTypeEnum.IdleSouthEast, string.Empty);
            this.AnimationResrefs.Add(AnimationTypeEnum.IdleSouthWest, string.Empty);
            this.AnimationResrefs.Add(AnimationTypeEnum.IdleWest, string.Empty);

            this.Scripts.Add(ScriptEventTypeEnum.OnCreatureConversation, string.Empty);
            this.Scripts.Add(ScriptEventTypeEnum.OnCreatureAttacked, string.Empty);
            this.Scripts.Add(ScriptEventTypeEnum.OnCreatureDamaged, string.Empty);
            this.Scripts.Add(ScriptEventTypeEnum.OnCreatureDeath, string.Empty);
            this.Scripts.Add(ScriptEventTypeEnum.OnCreatureDisturbed, string.Empty);
            this.Scripts.Add(ScriptEventTypeEnum.OnCreatureHeartbeat, string.Empty);
            this.Scripts.Add(ScriptEventTypeEnum.OnCreatureSpawned, string.Empty);

            this.EquippedItemResrefs.Add(InventorySlotEnum.Ammo, string.Empty);
            this.EquippedItemResrefs.Add(InventorySlotEnum.Arms, string.Empty);
            this.EquippedItemResrefs.Add(InventorySlotEnum.Back, string.Empty);
            this.EquippedItemResrefs.Add(InventorySlotEnum.Body, string.Empty);
            this.EquippedItemResrefs.Add(InventorySlotEnum.Head, string.Empty);
            this.EquippedItemResrefs.Add(InventorySlotEnum.MainHand, string.Empty);
            this.EquippedItemResrefs.Add(InventorySlotEnum.Neck, string.Empty);
            this.EquippedItemResrefs.Add(InventorySlotEnum.OffHand, string.Empty);
            this.EquippedItemResrefs.Add(InventorySlotEnum.Ring1, string.Empty);
            this.EquippedItemResrefs.Add(InventorySlotEnum.Ring2, string.Empty);
            this.EquippedItemResrefs.Add(InventorySlotEnum.Waist, string.Empty);
        }
    }
}
