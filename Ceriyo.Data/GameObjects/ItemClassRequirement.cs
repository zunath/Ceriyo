using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Ceriyo.Data.GameObjects
{
    public class ItemClassRequirement
    {
        [XmlIgnore]
        public CharacterClass CharacterClass
        {
            get
            {
                return WorkingDataManager.GetGameObject<CharacterClass>(ModulePaths.CharacterClassesDirectory, ClassResref);
            }
        }

        public string ClassResref { get; set; }
        public int LevelRequired { get; set; }
        public bool IsAvailable { get; set; }

        public ItemClassRequirement()
        {
            this.ClassResref = "";
            this.LevelRequired = EngineConstants.MaxLevel;
            this.IsAvailable = true;
        }
    
    }
}
