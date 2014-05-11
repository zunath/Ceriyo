﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ceriyo.Data;

namespace Ceriyo.Toolset.ValidationRules
{
    public class AreaHeightRule: NumericAmountRule
    {
        public AreaHeightRule()
        {
            this.Minimum = EngineConstants.AreaMinHeight;
            this.Maximum = EngineConstants.AreaMaxHeight;
        }
    }
}