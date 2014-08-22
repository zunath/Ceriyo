using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FlatRedBall;

namespace Ceriyo.Entities
{
    public class DrawableTile
    {
        public int Layer { get; set; }
        public int CellX { get; set; }
        public int CellY { get; set; }
        public Sprite TileSprite { get; set; }

        public DrawableTile()
        {
            this.Layer = 0;
            this.CellX = 0;
            this.CellY = 0;
            this.TileSprite = new Sprite();
        }
    }
}
