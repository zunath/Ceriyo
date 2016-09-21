using System;
using System.Collections.Generic;

namespace Ceriyo.Core.Data
{
    public class LocalVariableData
    {
        public string GlobalID { get; set; }
        public Dictionary<string, string> LocalStrings { get; set; }
        public Dictionary<string, float> LocalFloats { get; set; }

        public LocalVariableData()
        {
            GlobalID = Guid.NewGuid().ToString();
            LocalStrings = new Dictionary<string, string>();
            LocalFloats = new Dictionary<string, float>();
        }
    }
}
