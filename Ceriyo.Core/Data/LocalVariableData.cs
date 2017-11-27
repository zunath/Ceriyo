using System;
using System.Collections.Generic;
using Ceriyo.Core.Attributes;
using Ceriyo.Core.Data.Contracts;

namespace Ceriyo.Core.Data
{
    /// <summary>
    /// Stores local variable data.
    /// </summary>
    public class LocalVariableData : IDataDomainObject
    {
        /// <inheritdoc />
        public string GlobalID { get; set; }

        /// <inheritdoc />
        [SerializationIgnore]
        public string DirectoryName => null;

        /// <summary>
        /// The local strings stored.
        /// </summary>
        public List<LocalStringData> LocalStrings { get; set; }

        /// <summary>
        /// The local doubles stored.
        /// </summary>
        public List<LocalDoubleData> LocalDoubles { get; set; }

        /// <summary>
        /// Constructs a new local variables data object.
        /// </summary>
        public LocalVariableData()
        {
            GlobalID = Guid.NewGuid().ToString();
            LocalStrings = new List<LocalStringData>();
            LocalDoubles = new List<LocalDoubleData>();
        }
    }
}
