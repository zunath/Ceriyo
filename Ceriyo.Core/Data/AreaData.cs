using System;

namespace Ceriyo.Core.Data
{
    public class AreaData
    {
        public string GlobalID { get; set; }

        public string Name { get; set; }

        public string Tag { get; set; }

        public string Resref { get; set; }

        public string Description { get; set; }

        public string Comment { get; set; }

        public string OnAreaEnter { get; set; }

        public string OnAreaExit { get; set; }

        public string OnAreaHeartbeat { get; set; }

        public AreaData()
        {
            GlobalID = Guid.NewGuid().ToString();
        }
    }
}
