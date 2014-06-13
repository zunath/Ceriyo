using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Ceriyo.Data.GameObjects
{
    public class MapTile
    {
        public int MapX { get; set; }
        public int MapY { get; set; }
        public int Layer { get; set; }
        public int TileDefinitionX { get; set; }
        public int TileDefinitionY { get; set; }
        public bool HasGraphic { get; set; }

        public MapTile()
        {
            this.MapX = 0;
            this.MapY = 0;
            this.Layer = 0;
            this.TileDefinitionX = 0;
            this.TileDefinitionY = 0;
            this.HasGraphic = false;
        }

        public MapTile(int mapX, int mapY, int layer)
        {
            this.MapX = mapX;
            this.MapY = mapY;
            this.Layer = layer;
            this.TileDefinitionX = 0;
            this.TileDefinitionY = 0;
            this.HasGraphic = false;
        }
    }
}
