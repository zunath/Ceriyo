using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ceriyo.Data.EventArguments
{
    public class TilePaintEventArgs : EventArgs
    {
        public int CellX { get; set; }
        public int CellY { get; set; }

        public TilePaintEventArgs(int cellX, int cellY)
        {
            this.CellX = cellX;
            this.CellY = cellY;
        }
    }
}
