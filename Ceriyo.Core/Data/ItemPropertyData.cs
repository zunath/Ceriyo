using System;
using Ceriyo.Core.Data.Contracts;

namespace Ceriyo.Core.Data
{
    public class ItemPropertyData : IDataDomainObject
    {
        public string GlobalID { get; set; }

        public string Name { get; set; }

        public string Tag { get; set; }

        public string Resref { get; set; }

        public string Description { get; set; }

        public string Comment { get; set; }

        public ItemPropertyData()
        {
            GlobalID = Guid.NewGuid().ToString();
        }
    }
}
