﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace Ceriyo.Data.EventArguments
{
    public class TilePaintEventArgs : EventArgs
    {
        public int Layer { get; set; }
        public int StartCellX { get; set; }
        public int StartCellY { get; set; }
        public int EndCellX { get; set; }
        public int EndCellY { get; set; }

        public Texture2D Texture { get; set; }

        public TilePaintEventArgs(int startCellX, 
                                 int startCellY, 
                                 int endCellX, 
                                 int endCellY, 
                                 int layer, 
                                 Texture2D texture)
        {
            this.Layer = layer;
            this.StartCellX = startCellX;
            this.StartCellY = startCellY;
            this.EndCellX = endCellX;
            this.EndCellY = endCellY;
            this.Texture = texture;
        }
    }
}
