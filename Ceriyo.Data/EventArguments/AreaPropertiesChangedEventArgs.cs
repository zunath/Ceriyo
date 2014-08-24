using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ceriyo.Data.GameObjects;

namespace Ceriyo.Data.EventArguments
{
    public class AreaPropertiesChangedEventArgs : EventArgs
    {
        public Area ModifiedArea { get; set; }
        public bool IsUpdate { get; set; }

        public AreaPropertiesChangedEventArgs(Area modifiedArea, bool isUpdate)
        {
            this.ModifiedArea = modifiedArea;
            this.IsUpdate = isUpdate;

        }
    }
}
