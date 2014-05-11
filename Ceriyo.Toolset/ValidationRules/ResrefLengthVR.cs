using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ceriyo.Data;

namespace Ceriyo.Toolset.ValidationRules
{
    public class ResrefLengthVR : TextLengthVR
    {
        public ResrefLengthVR()
        {
            this.MinimumLength = 1;
            this.MaximumLength = EngineConstants.ResrefMaxLength;
        }
    }
}
