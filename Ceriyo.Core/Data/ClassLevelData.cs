using System;
using Ceriyo.Core.Attributes;
using Ceriyo.Core.Data.Contracts;

namespace Ceriyo.Core.Data
{
    public class ClassLevelData: IDataDomainObject
    {
        public int Level { get; set; }
        public int ExperienceRequired { get; set; }
        public string GlobalID { get; set; }
        [SerializationIgnore]
        public string DirectoryName => null;

        public ClassLevelData()
        {
            GlobalID = Guid.NewGuid().ToString();
        }
    }
}
