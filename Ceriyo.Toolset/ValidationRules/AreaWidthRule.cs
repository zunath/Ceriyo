using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ceriyo.Data;
using Ceriyo.Data.Engine;

namespace Ceriyo.Toolset.ValidationRules
{
    public class AreaWidthRule : NumericAmountRule
    {
        public AreaWidthRule()
        {
            this.Minimum = EngineConstants.AreaMinWidth;
            this.Maximum = EngineConstants.AreaMaxWidth;
        }
    }
}
