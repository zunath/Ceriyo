using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ceriyo.Data.GameObjects;

namespace Ceriyo.Data.EventArguments
{
    public class AreaEventArgs : EventArgs
    {
        public Area Area { get; set; }

        public AreaEventArgs(Area area)
        {
            this.Area = area;
        }
    }
}
