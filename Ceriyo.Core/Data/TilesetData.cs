using System;
using Ceriyo.Core.Attributes;
using Ceriyo.Core.Data.Contracts;

namespace Ceriyo.Core.Data
{
    /// <summary>
    /// Stores tileset data.
    /// </summary>
    public class TilesetData : IDataDomainObject
    {
        /// <inheritdoc />
        public string GlobalID { get; set; }

        /// <inheritdoc />
        [SerializationIgnore]
        public string DirectoryName => "Tileset";

        /// <summary>
        /// The name of the tileset.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The tag of the tileset.
        /// </summary>
        public string Tag { get; set; }

        /// <summary>
        /// The resref of the tileset.
        /// </summary>
        public string Resref { get; set; }

        /// <summary>
        /// The description of the tileset.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The comment of the tileset.
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// The name of the resource used by the tileset.
        /// </summary>
        public string ResourceName { get; set; }

        /// <summary>
        /// Constructs a new tileset data object.
        /// </summary>
        public TilesetData()
        {
            GlobalID = Guid.NewGuid().ToString();
        }
    }
}
