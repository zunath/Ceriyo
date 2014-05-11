using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ceriyo.Data;

namespace Ceriyo.Toolset.ValidationRules
{
    public class AreaHeightVR: NumericAmountVR
    {
        public AreaHeightVR()
        {
            this.Minimum = EngineConstants.AreaMinHeight;
            this.Maximum = EngineConstants.AreaMaxHeight;
        }
    }
}
