using Ceriyo.Core.Data.Contracts;

namespace Ceriyo.Core.Data
{
    public class ClassLevelData : IDataDomainObject
    {
        public int Level { get; set; }
        public int ExperienceRequired { get; set; }
    }
}
