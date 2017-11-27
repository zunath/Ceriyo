using System;
using Ceriyo.Core.Attributes;
using Ceriyo.Core.Data.Contracts;

namespace Ceriyo.Core.Data
{
    /// <summary>
    /// Stores item type data
    /// </summary>
    public class ItemTypeData : IDataDomainObject
    {
        /// <inheritdoc />
        public string GlobalID { get; set; }

        /// <inheritdoc />
        [SerializationIgnore]
        public string DirectoryName => "ItemType";

        /// <summary>
        /// The name of the item type.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The tag of the item type.
        /// </summary>
        public string Tag { get; set; }

        /// <summary>
        /// The resref of the item type.
        /// </summary>
        public string Resref { get; set; }

        /// <summary>
        /// Constructs a new item type data object.
        /// </summary>
        public ItemTypeData()
        {
            GlobalID = Guid.NewGuid().ToString();
        }
    }
}
