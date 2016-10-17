using System;

namespace Ceriyo.Core.Data
{
    public class ScriptData
    {
        public string GlobalID { get; set; }

        public string Name { get; set; }

        public string Resref { get; set; }

        public ScriptData()
        {
            GlobalID = Guid.NewGuid().ToString();
        }
    }
}
