using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

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
