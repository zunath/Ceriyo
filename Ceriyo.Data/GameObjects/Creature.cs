using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Ceriyo.Data.Enumerations;
using Ceriyo.Data.Extensions;

namespace Ceriyo.Data.GameObjects
{
    public class Creature: IGameObject
    {
        [XmlIgnore]
        private WorkingDataManager WorkingManager { get; set; }

        public string Name { get; set; }
        public string Tag { get; set; }
        public string Resref { get; set; }
        public string Description { get; set; }
        public string Comments { get; set; }
        [XmlIgnore]
        public string WorkingDirectory { get { return WorkingPaths.CreaturesDirectory; } }
        public BindingList<LocalVariable> LocalVariables { get; set; }
        public SerializableDictionary<ScriptEventTypeEnum, string> Scripts { get; set; }
        [XmlIgnore]
        public string CategoryName { get { return "Script"; } }
        public SerializableDictionary<AnimationTypeEnum, string> AnimationResrefs { get; set; }

        public string CharacterClassResref { get; set; }
        public int HitPoints { get; set; }
        public int Mana { get; set; }
        public int Strength { get; set; }
        public int Dexterity { get; set; }
        public int Constitution { get; set; }
        public int Wisdom { get; set; }
        public int Intelligence { get; set; }
        public int Charisma { get; set; }
        public GenderTypeEnum Gender { get; set; }

        public BindingList<string> AbilityResrefs { get; set; }
        public BindingList<string> SkillResrefs { get; set; }
        public BindingList<string> ItemResrefs { get; set; }

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
            this.HitPoints = 0;
            this.Strength = 0;
            this.Dexterity = 0;
            this.Constitution = 0;
            this.Wisdom = 0;
            this.Intelligence = 0;
            this.Charisma = 0;

            this.Gender = GenderTypeEnum.Male;
            this.AbilityResrefs = new BindingList<string>();
            this.SkillResrefs = new BindingList<string>();
            this.ItemResrefs = new BindingList<string>();

        }
    }
}
