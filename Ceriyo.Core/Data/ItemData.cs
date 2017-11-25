using System;
using System.Collections.Generic;
using Ceriyo.Core.Attributes;
using Ceriyo.Core.Data.Contracts;

namespace Ceriyo.Core.Data
{
    public class ItemData : IDataDomainObject
    {
        public string GlobalID { get; set; }
        [SerializationIgnore]
        public string DirectoryName => "Item";

        public string Name { get; set; }

        public string Tag { get; set; }

        public string Resref { get; set; }

        public string ItemTypeResref { get; set; }

        public bool IsUndroppable { get; set; }

        public bool IsStolen { get; set; }

        public bool IsPlot { get; set; }

        public string Description { get; set; }

        public string Comment { get; set; }


        public string OnActivated { get; set; }

        public string OnAcquired { get; set; }

        public string OnUnacquired { get; set; }

        public string OnEquipped { get; set; }

        public string OnUnequipped { get; set; }

        public LocalVariableData LocalVariables { get; set; }

        public List<ClassRequirementData> ClassRequirements { get; set; }

        public List<string> ItemPropertyResrefs { get; set; }

        public ItemData()
        {
            GlobalID = Guid.NewGuid().ToString();
            LocalVariables = new LocalVariableData();
            ClassRequirements = new List<ClassRequirementData>();
            ItemPropertyResrefs = new List<string>();
        }
    }
}
