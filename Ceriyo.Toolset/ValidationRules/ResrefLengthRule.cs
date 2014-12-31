using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ceriyo.Data;
using Ceriyo.Data.Engine;

namespace Ceriyo.Toolset.ValidationRules
{
    public class ResrefLengthRule : TextLengthRule
    {
        public ResrefLengthRule()
        {
            this.MinimumLength = 1;
            this.MaximumLength = EngineConstants.ResrefMaxLength;
        }
    }
}
