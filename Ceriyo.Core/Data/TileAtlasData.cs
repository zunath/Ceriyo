using System;
using System.Collections.Generic;

namespace Ceriyo.Core.Data
{
    /// <summary>
    /// Stores tile atlas data.
    /// Tracks tile locations using the X,Y coordinates.
    /// </summary>
    public class TileAtlasData
    {
        /// <summary>
        /// The raw tile data.
        /// </summary>
        public Dictionary<Tuple<int, int>, TileData> Tiles { get; set; }

        /// <summary>
        /// Constructs a new tile atlas data object.
        /// </summary>
        public TileAtlasData()
        {
            Tiles = new Dictionary<Tuple<int, int>, TileData>();
        }

        /// <summary>
        /// Sets a tile at a specified X and Y cell coordinate.
        /// </summary>
        /// <param name="x">The X cell coordinate</param>
        /// <param name="y">The Y cell coordinate</param>
        /// <param name="tile">The tile to set at the coordinate.</param>
        public void SetTile(int x, int y, TileData tile)
        {
            Tuple<int, int> key = new Tuple<int, int>(x, y);
            if (Tiles.ContainsKey(key))
            {
                Tiles[key] = tile;
            }
            else
            {
                Tiles.Add(key, tile);
            }
        }

        /// <summary>
        /// Returns the tile at a specified X and Y cell coordinate.
        /// </summary>
        /// <param name="x">The X cell coordinate</param>
        /// <param name="y">The Y cell coordinate</param>
        /// <returns>The tile at the coordinate.</returns>
        public TileData GetTile(int x, int y)
        {
            Tuple<int, int> key = new Tuple<int, int>(x, y);

            if (Tiles.ContainsKey(key))
            {
                return Tiles[key];
            }

            return null;
        }

    }
}
