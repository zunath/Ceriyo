using System;
using Ceriyo.Core.Attributes;
using Ceriyo.Core.Data.Contracts;

namespace Ceriyo.Core.Data
{
    /// <summary>
    /// Stores class data
    /// </summary>
    public class ClassData : IDataDomainObject
    {
        /// <inheritdoc />
        public string GlobalID { get; set; }

        /// <inheritdoc />
        [SerializationIgnore]
        public string DirectoryName => "Class";

        /// <summary>
        /// Name of the class.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Tag of the class.
        /// </summary>
        public string Tag { get; set; }

        /// <summary>
        /// Resref of the class.
        /// </summary>
        public string Resref { get; set; }

        /// <summary>
        /// Constructs a new class data object
        /// </summary>
        public ClassData()
        {
            GlobalID = Guid.NewGuid().ToString();
        }
    }
}
