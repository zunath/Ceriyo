using System;

namespace Ceriyo.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Field)]
    public class ColorTypeAttribute : Attribute
    {
        public int Red { get; set; }
        public int Green { get; set; }
        public int Blue { get; set; }
        public int Alpha { get; set; }

        public ColorTypeAttribute(int red, int green, int blue, int alpha)
        {
            Red = red;
            Green = green;
            Blue = blue;
            Alpha = alpha;
        }
    }
}
