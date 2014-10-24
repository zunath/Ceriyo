using System.ComponentModel;

namespace Ceriyo.Data.Enumerations
{
    public enum PVPTypeEnum : byte
    {
        [Description("None")]
        None = 1,
        [Description("Party")]
        Party = 2,
        [Description("Full")]
        Full = 3
    }
}
