using System.Xml.Serialization;
using Ceriyo.Data.Annotations;

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

        // These variables are temporarily used in the data editor to store necessary
        // info without writing this object to disk.
        private readonly string _className;

        [XmlIgnore, UsedImplicitly]
        public string ClassName
        {
            get
            {
                return IsOnDisk ? CharacterClass.Name : _className;
            }
        }
        [XmlIgnore]
        private bool IsOnDisk { get; set; }

        public string ClassResref { get; set; }
        public int LevelRequired { get; set; }
        public bool IsAvailable { get; set; }

        public ItemClassRequirement()
        {
            ClassResref = string.Empty;
            LevelRequired = EngineConstants.MaxLevel;
            IsAvailable = true;
            WorkingManager = new WorkingDataManager();
            IsOnDisk = true;
        }

        public ItemClassRequirement(bool isOnDisk = true, string className = "")
        {
            IsOnDisk = isOnDisk;
            ClassResref = string.Empty;
            LevelRequired = EngineConstants.MaxLevel;
            IsAvailable = true;
            WorkingManager = new WorkingDataManager();
            _className = className;
        }
    
    }
}
