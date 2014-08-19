using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Squid;

namespace Ceriyo.Data.ResourceObjects
{
    public class UILayout
    {
        public List<UIComponent> Components { get; set; }

        public UILayout()
        {
            this.Components = new List<UIComponent>();
        }
    }
}
