using Ceriyo.Core.Attributes;

namespace Ceriyo.Core.Constants
{
    /// <summary>
    /// Pre-defined color types for use in scripting.
    /// </summary>
    public enum ColorType
    {
        /// <summary>
        /// White color (255, 255, 255, 255)
        /// </summary>
        [ColorType(255, 255, 255, 255)]
        White,

        /// <summary>
        /// Red color (255, 0, 0, 255)
        /// </summary>
        [ColorType(255, 0, 0, 255)]
        Red,

        /// <summary>
        /// Green color (0, 255, 0, 255)
        /// </summary>
        [ColorType(0, 255, 0, 255)]
        Green,

        /// <summary>
        /// Blue color (0, 0, 255, 255)
        /// </summary>
        [ColorType(0, 0, 255, 255)]
        Blue,

        /// <summary>
        /// Orange color (255, 165, 0, 255)
        /// </summary>
        [ColorType(255, 165, 0, 255)]
        Orange,

        /// <summary>
        /// Yellow color (255, 255, 0, 255)
        /// </summary>
        [ColorType(255, 255, 0, 255)]
        Yellow,

        /// <summary>
        /// Tan color (210, 180, 140, 255)
        /// </summary>
        [ColorType(210, 180, 140, 255)]
        Tan,

        /// <summary>
        /// Violet color (238, 130, 238, 255)
        /// </summary>
        [ColorType(238, 130, 238, 255)]
        Violet,

        /// <summary>
        /// Gray color (128, 128, 128, 255)
        /// </summary>
        [ColorType(128, 128, 128, 255)]
        Gray,

        /// <summary>
        /// Black color (0, 0, 0, 255)
        /// </summary>
        [ColorType(0, 0, 0, 255)]
        Black,

        /// <summary>
        /// Pink color (255, 192, 203, 255)
        /// </summary>
        [ColorType(255, 192, 203, 255)]
        Pink
    }
}
