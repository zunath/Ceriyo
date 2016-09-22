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
            if (red < 0 || red > 255) throw new ArgumentException("Value for 'red' should be between 0 and 255.");
            if (green < 0 || green > 255) throw new ArgumentException("Value for 'green' should be between 0 and 255.");
            if (blue < 0 || blue > 255) throw new ArgumentException("Value for 'blue' should be between 0 and 255.");
            if (alpha < 0 || alpha > 255) throw new ArgumentException("Value for 'alpha' should be between 0 and 255.");

            Red = red;
            Green = green;
            Blue = blue;
            Alpha = alpha;
        }
    }
}
