using System;
using Ceriyo.Core.Attributes;
using Ceriyo.Core.Data.Contracts;

namespace Ceriyo.Core.Data
{
    /// <summary>
    /// Stores area data
    /// </summary>
    public class AreaData : IDataDomainObject
    {
        /// <inheritdoc />
        public string GlobalID { get; set; }

        /// <inheritdoc />
        [SerializationIgnore]
        public string DirectoryName => "Area";

        /// <summary>
        /// The name of the area.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The tag of the area.
        /// </summary>
        public string Tag { get; set; }

        /// <summary>
        /// The resref of the area.
        /// </summary>
        public string Resref { get; set; }

        /// <summary>
        /// The description of the area.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The comment of the area
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// The script used when a player enters the area.
        /// </summary>
        public string OnAreaEnter { get; set; }

        /// <summary>
        /// The script used when a player leaves the area.
        /// </summary>
        public string OnAreaExit { get; set; }

        /// <summary>
        /// The script used when the heartbeat fires.
        /// </summary>
        public string OnAreaHeartbeat { get; set; }

        /// <summary>
        /// The width of the area, in tiles.
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// The height of the area, in tiles.
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// The local variables stored on the area.
        /// </summary>
        public LocalVariableData LocalVariables { get; set; }

        /// <summary>
        /// The global ID of the tileset being used by the area.
        /// </summary>
        public string TilesetGlobalID { get; set; }

        /// <summary>
        /// The tile atlas data used by the area.
        /// </summary>
        public TileAtlasData TileAtlas { get; set; }

        /// <summary>
        /// Constructs a new area data object.
        /// </summary>
        public AreaData()
        {
            GlobalID = Guid.NewGuid().ToString();
            LocalVariables = new LocalVariableData();
            TileAtlas = new TileAtlasData();
        }
    }
}
