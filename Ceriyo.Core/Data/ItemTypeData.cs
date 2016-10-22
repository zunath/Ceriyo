using System;
using Ceriyo.Core.Data.Contracts;

namespace Ceriyo.Core.Data
{
    public class ItemTypeData : IDataDomainObject
    {
        public string GlobalID { get; set; }
        public string Name { get; set; }
        public string Tag { get; set; }
        public string Resref { get; set; }

        public ItemTypeData()
        {
            GlobalID = Guid.NewGuid().ToString();
        }
    }
}
