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
        public bool IsVisible { get; set; }
        public TileDefinition Definition { get; set; }

        public MapTile()
        {
            this.MapX = 0;
            this.MapY = 0;
            this.Layer = 0;
            this.IsVisible = true;
            this.Definition = new TileDefinition();
        }

        public MapTile(int mapX, int mapY, int layer)
        {
            this.MapX = mapX;
            this.MapY = mapY;
            this.Layer = layer;
            this.IsVisible = true;
            this.Definition = new TileDefinition();
        }
    }
}
