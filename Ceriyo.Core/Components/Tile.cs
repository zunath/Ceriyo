using Artemis.Interface;

namespace Ceriyo.Core.Components
{
    /// <summary>
    /// Tracks a tile's source information
    /// </summary>
    public class Tile: IComponent
    {
        /// <summary>
        /// The X position of the tile source
        /// </summary>
        public int SourceX { get; set; }
        /// <summary>
        /// The Y position of the tile source.
        /// </summary>
        public int SourceY { get; set; }
    }
}
