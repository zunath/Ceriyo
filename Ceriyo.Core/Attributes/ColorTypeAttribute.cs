using System;

namespace Ceriyo.Core.Attributes
{
    /// <summary>
    /// Attribute which describes a specific color. All values are on a scale from 0 to 255
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class ColorTypeAttribute : Attribute
    {
        /// <summary>
        /// The red value
        /// </summary>
        public int Red { get; set; }
        /// <summary>
        /// The green value
        /// </summary>
        public int Green { get; set; }
        /// <summary>
        /// The blue value
        /// </summary>
        public int Blue { get; set; }
        /// <summary>
        /// The alpha value
        /// </summary>
        public int Alpha { get; set; }

        /// <summary>
        /// Constructs a new ColorTypeAttribute
        /// </summary>
        /// <param name="red">The red value (0-255)</param>
        /// <param name="green">The green value (0-255)</param>
        /// <param name="blue">The blue value (0-255)</param>
        /// <param name="alpha">The alpha value (0-255)</param>
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
