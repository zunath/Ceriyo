using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace Ceriyo.Data.EventArguments
{
    public class TilePaintEventArgs : EventArgs
    {
        public int Layer { get; set; }
        public int CellX { get; set; }
        public int CellY { get; set; }

        public Texture2D Texture { get; set; }

        public TilePaintEventArgs(int cellX, int cellY, int layer, Texture2D texture)
        {
            this.Layer = layer;
            this.CellX = cellX;
            this.CellY = cellY;
            this.Texture = texture;
        }
    }
}
