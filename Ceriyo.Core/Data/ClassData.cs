using System;

namespace Ceriyo.Core.Data
{
    public class ClassData
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
