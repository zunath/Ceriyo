﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ceriyo.Data.GameObjects
{
    public class Tile
    {
        public int TextureCellX { get; set; }
        public int TextureCellY { get; set; }
        public int Priority { get; set; }
        public bool IsVisible { get; set; }
        public bool TopLeftPassable { get; set; }
        public bool TopRightPassable { get; set; }
        public bool BottomLeftPassable { get; set; }
        public bool BottomRightPassable { get; set; }

        public Tile()
        {
            this.TextureCellX = 0;
            this.TextureCellY = 0;
            this.Priority = 0;
            this.IsVisible = true;
            this.TopLeftPassable = true;
            this.TopRightPassable = true;
            this.BottomLeftPassable = true;
            this.BottomRightPassable = true;
        }
    }
}
