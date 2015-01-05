using System;

namespace Ceriyo.Data.Enumerations
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
