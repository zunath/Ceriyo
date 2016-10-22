using System;
using System.Collections.Generic;
using Ceriyo.Core.Data.Contracts;

namespace Ceriyo.Core.Data
{
    public class LocalVariableData : IDataDomainObject
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
