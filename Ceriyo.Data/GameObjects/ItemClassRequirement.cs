using System.Xml.Serialization;
using Ceriyo.Data.Annotations;

namespace Ceriyo.Data.GameObjects
{
    public class ItemClassRequirement
    {
        public string ClassName { get; set; }
        public string ClassResref { get; set; }
        public int LevelRequired { get; set; }
        public bool IsAvailable { get; set; }

        public ItemClassRequirement()
        {
            ClassName = string.Empty;
            ClassResref = string.Empty;
            LevelRequired = EngineConstants.MaxLevel;
            IsAvailable = true;
        }

        public ItemClassRequirement(bool isOnDisk = true, string className = "")
        {
            ClassName = className;
            ClassResref = string.Empty;
            LevelRequired = EngineConstants.MaxLevel;
            IsAvailable = true;
        }
    
    }
}
