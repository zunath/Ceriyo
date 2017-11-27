using Artemis.Interface;

namespace Ceriyo.Core.Components
{
    /// <summary>
    /// Tracks a map's details
    /// </summary>
    public class Map: IComponent
    {
        /// <summary>
        /// Width of the map
        /// </summary>
        public int Width => Tiles.GetUpperBound(0) + 1;
        /// <summary>
        /// Height of the map
        /// </summary>
        public int Height => Tiles.GetUpperBound(1) + 1;
        /// <summary>
        /// Tiles on the map.
        /// </summary>
        public Tile[,] Tiles { get; set; }

        /// <summary>
        /// Resizes the map to a new width and height. 
        /// If the width or height is smaller than before, some tile data will be lost.
        /// </summary>
        /// <param name="newWidth">The new width of the map.</param>
        /// <param name="newHeight">The new height of the map.</param>
        public void Resize(int newWidth, int newHeight)
        {
            Tile[,] newTiles = new Tile[newWidth, newHeight];

            for (int x = 0; x < newWidth; x++)
            {
                for (int y = 0; y < newHeight; y++)
                {
                    if (Width > x && Height > y)
                    {
                        newTiles[x, y] = Tiles[x, y];
                    }
                }
            }

            Tiles = newTiles;
        }

    }
}
