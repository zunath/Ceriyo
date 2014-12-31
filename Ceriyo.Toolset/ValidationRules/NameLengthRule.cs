using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ceriyo.Data;
using Ceriyo.Data.Engine;

namespace Ceriyo.Toolset.ValidationRules
{
    public class NameLengthRule : TextLengthRule
    {
        public NameLengthRule()
        {
            this.MinimumLength = 1;
            this.MaximumLength = EngineConstants.NameMaxLength;
        }
    }
}
