using System;
using Ceriyo.Core.Attributes;
using Ceriyo.Core.Data.Contracts;

namespace Ceriyo.Core.Data
{
    /// <summary>
    /// Stores item property data.
    /// </summary>
    public class ItemPropertyData : IDataDomainObject
    {
        /// <inheritdoc />
        public string GlobalID { get; set; }

        /// <inheritdoc />
        [SerializationIgnore]
        public string DirectoryName => "ItemProperty";

        /// <summary>
        /// The name of the item property.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The tag of the item property.
        /// </summary>
        public string Tag { get; set; }

        /// <summary>
        /// The resref of the item property.
        /// </summary>
        public string Resref { get; set; }

        /// <summary>
        /// The description of the item property.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The comment of the item property.
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// Constructs a new item property data object.
        /// </summary>
        public ItemPropertyData()
        {
            GlobalID = Guid.NewGuid().ToString();
        }
    }
}
