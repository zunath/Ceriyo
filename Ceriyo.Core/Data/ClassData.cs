using System;
using Ceriyo.Core.Data.Contracts;

namespace Ceriyo.Core.Data
{
    public class ClassData : IDataDomainObject
    {
        public string GlobalID { get; set; }

        public string Name { get; set; }

        public string Tag { get; set; }

        public string Resref { get; set; }

        public ClassData()
        {
            GlobalID = Guid.NewGuid().ToString();
        }
    }
}
