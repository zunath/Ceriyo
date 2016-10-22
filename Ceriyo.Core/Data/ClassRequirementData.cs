using Ceriyo.Core.Data.Contracts;

namespace Ceriyo.Core.Data
{
    public class ClassRequirementData : IDataDomainObject
    {
        public string ClassResref { get; set; }

        public int LevelRequired { get; set; }
    }
}
