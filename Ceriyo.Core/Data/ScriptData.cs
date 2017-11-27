using System;
using Ceriyo.Core.Attributes;
using Ceriyo.Core.Data.Contracts;

namespace Ceriyo.Core.Data
{
    /// <summary>
    /// Stores script data.
    /// </summary>
    public class ScriptData : IDataDomainObject
    {
        /// <inheritdoc />
        public string GlobalID { get; set; }

        /// <inheritdoc />
        [SerializationIgnore]
        public string DirectoryName => "Script";

        /// <summary>
        /// The name of the script.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The resref of the script.
        /// </summary>
        public string Resref { get; set; }

        /// <summary>
        /// Constructs a new script data object.
        /// </summary>
        public ScriptData()
        {
            GlobalID = Guid.NewGuid().ToString();
        }
    }
}
