using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ceriyo.Data;

namespace Ceriyo.Toolset.ValidationRules
{
    public class TagLengthVR : TextLengthVR
    {
        public TagLengthVR()
        {
            this.MinimumLength = 1;
            this.MaximumLength = EngineConstants.TagMaxLength;
        }
    }
}
