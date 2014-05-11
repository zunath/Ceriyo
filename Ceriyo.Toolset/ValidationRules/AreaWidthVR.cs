using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ceriyo.Data;

namespace Ceriyo.Toolset.ValidationRules
{
    public class AreaWidthVR : NumericAmountVR
    {
        public AreaWidthVR()
        {
            this.Minimum = EngineConstants.AreaMinWidth;
            this.Maximum = EngineConstants.AreaMaxWidth;
        }
    }
}
