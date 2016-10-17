using System;

namespace Ceriyo.Core.Data
{
    public class AbilityData
    {
        public string GlobalID { get; set; }

        public string Name { get; set; }

        public string Tag { get; set; }

        public string Resref { get; set; }

        public bool IsPassive { get; set; }

        public string OnActivated { get; set; }

        public string Description { get; set; }

        public string Comment { get; set; }


        public AbilityData()
        {
            GlobalID = Guid.NewGuid().ToString();
        }
    }
}
