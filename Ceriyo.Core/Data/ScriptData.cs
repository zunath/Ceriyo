using System;
using Ceriyo.Core.Data.Contracts;

namespace Ceriyo.Core.Data
{
    public class ScriptData : IDataDomainObject
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
