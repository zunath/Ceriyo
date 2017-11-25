using System;
using Ceriyo.Core.Attributes;
using Ceriyo.Core.Data.Contracts;

namespace Ceriyo.Core.Data
{
    public class DialogData : IDataDomainObject
    {
        public string GlobalID { get; set; }
        [SerializationIgnore]
        public string DirectoryName => "Dialog";
        public string Name { get; set; }
        public string Tag { get; set; }
        public string Resref { get; set; }

        public DialogData()
        {
            GlobalID = Guid.NewGuid().ToString();
        }
    }
}
