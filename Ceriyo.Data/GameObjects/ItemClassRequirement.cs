using System.Xml.Serialization;

namespace Ceriyo.Data.GameObjects
{
    public class ItemClassRequirement
    {
        [XmlIgnore]
        private WorkingDataManager WorkingManager { get; set; }

        [XmlIgnore]
        public CharacterClass CharacterClass
        {
            get
            {
                return WorkingManager.GetGameObject<CharacterClass>(ModulePaths.CharacterClassesDirectory, ClassResref);
            }
        }

        public string ClassResref { get; set; }
        public int LevelRequired { get; set; }
        public bool IsAvailable { get; set; }

        public ItemClassRequirement()
        {
            ClassResref = string.Empty;
            LevelRequired = EngineConstants.MaxLevel;
            IsAvailable = true;
            WorkingManager = new WorkingDataManager();
        }
    
    }
}
