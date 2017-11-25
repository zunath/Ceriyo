using System;
using System.Collections.Generic;
using Ceriyo.Core.Data.Contracts;

namespace Ceriyo.Core.Data
{
    public class TileAtlasData
    {
        public Dictionary<Tuple<int, int>, TileData> Tiles { get; set; }

        public TileAtlasData()
        {
            Tiles = new Dictionary<Tuple<int, int>, TileData>();
        }

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
