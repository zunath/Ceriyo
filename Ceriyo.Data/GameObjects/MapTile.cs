using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ceriyo.Data.GameObjects
{
    public class MapTile
    {
        public int MapX { get; set; }
        public int MapY { get; set; }
        public int Layer { get; set; }
        public Tile Tile { get; set; }

        public MapTile()
        {
            this.MapX = 0;
            this.MapY = 0;
            this.Layer = 0;
            this.Tile = new Tile();
        }

        public MapTile(int mapX, int mapY, int layer)
        {
            this.MapX = mapX;
            this.MapY = mapY;
            this.Layer = layer;
            this.Tile = new Tile();
        }
    }
}
