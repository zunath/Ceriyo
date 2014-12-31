using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ceriyo.Data;
using Ceriyo.Data.Engine;

namespace Ceriyo.Toolset.ValidationRules
{
    public class MaxLevelRule : NumericAmountRule
    {
        public MaxLevelRule()
        {
            this.Minimum = 1;
            this.Maximum = EngineConstants.MaxLevel;
        }
    }
}
