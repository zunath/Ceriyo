using System;
using Ceriyo.Data.GameObjects;

namespace Ceriyo.Data.EventArguments
{
    public class AreaPropertiesChangedEventArgs : EventArgs
    {
        public Area ModifiedArea { get; set; }
        public bool IsUpdate { get; set; }

        public AreaPropertiesChangedEventArgs(Area modifiedArea, bool isUpdate)
        {
            ModifiedArea = modifiedArea;
            IsUpdate = isUpdate;

        }
    }
}
