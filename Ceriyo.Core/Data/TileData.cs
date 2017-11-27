namespace Ceriyo.Core.Data
{
    /// <summary>
    /// Stores tile source data.
    /// These coordinates are the locations on the sprite sheet of where to find the image.
    /// </summary>
    public class TileData
    {
        /// <summary>
        /// The X coordinate on the sprite sheet.
        /// </summary>
        public int SourceX { get; set; }

        /// <summary>
        /// The Y coordinate on the sprite sheet.
        /// </summary>
        public int SourceY { get; set; }
    }
}
