using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ceriyo.Data.GameObjects
{
    public class Tile
    {
        public int TextureCellX { get; set; }
        public int TextureCellY { get; set; }
        public bool IsVisible { get; set; }

        public Tile()
        {
            this.TextureCellX = 0;
            this.TextureCellY = 0;
            this.IsVisible = true;
        }
    }
}
