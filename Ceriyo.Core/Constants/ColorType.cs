using Ceriyo.Core.Attributes;

namespace Ceriyo.Core.Constants
{
    public enum ColorType
    {
        [ColorType(255, 255, 255, 255)]
        White,
        [ColorType(255, 0, 0, 255)]
        Red,
        [ColorType(0, 255, 0, 255)]
        Green,
        [ColorType(0, 0, 255, 255)]
        Blue,
        [ColorType(255, 165, 0, 255)]
        Orange,
        [ColorType(255, 255, 0, 255)]
        Yellow,
        [ColorType(210, 180, 140, 255)]
        Tan,
        [ColorType(238, 130, 238, 255)]
        Violet,
        [ColorType(128, 128, 128, 255)]
        Gray,
        [ColorType(0, 0, 0, 255)]
        Black,
        [ColorType(255, 192, 203, 255)]
        Pink
    }
}
