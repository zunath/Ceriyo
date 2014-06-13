﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ceriyo.Data.GameObjects
{
    public class TileDefinition
    {
        public int TextureCellX { get; set; }
        public int TextureCellY { get; set; }
        public int Priority { get; set; }
        public bool IsPassable { get; set; }

        public TileDefinition()
        {
            this.TextureCellX = 0;
            this.TextureCellY = 0;
            this.Priority = 0;
            this.IsPassable = true;
        }
    }
}
