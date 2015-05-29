using System;

namespace Ceriyo.Data.EventArguments
{
    public class SimpleTypesEventArgs : EventArgs
    {
        public int IntegerValue { get; set; }

        public SimpleTypesEventArgs(int integerValue)
        {
            IntegerValue = integerValue;
        }
    }
}
