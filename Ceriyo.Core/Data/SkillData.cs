using System;
using Ceriyo.Core.Attributes;
using Ceriyo.Core.Data.Contracts;

namespace Ceriyo.Core.Data
{
    public class SkillData : IDataDomainObject
    {
        public string GlobalID { get; set; }
        [SerializationIgnore]
        public string DirectoryName => "Skill";

        public string Name { get; set; }

        public string Tag { get; set; }

        public string Resref { get; set; }

        public string Description { get; set; }

        public string Comment { get; set; }

        public bool IsPassive { get; set; }

        public string OnActivated { get; set; }


        public SkillData()
        {
            GlobalID = Guid.NewGuid().ToString();
        }
    }
}
