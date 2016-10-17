using System;
using System.Collections.Generic;

namespace Ceriyo.Core.Data
{
    public class LocalVariableData
    {
        public string GlobalID { get; set; }

        public List<LocalStringData> LocalStrings { get; set; }

        public List<LocalDoubleData> LocalDoubles { get; set; }

        public LocalVariableData()
        {
            GlobalID = Guid.NewGuid().ToString();
            LocalStrings = new List<LocalStringData>();
            LocalDoubles = new List<LocalDoubleData>();
        }
    }
}
