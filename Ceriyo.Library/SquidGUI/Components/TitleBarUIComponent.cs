using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Squid;

namespace Ceriyo.Library.SquidGUI.Components
{
    public class TitleBarUIComponent : Label
    {
        public Button Button { get; private set; }

        public TitleBarUIComponent(bool allowClose)
        {
            Button = new Button();
            Button.Size = new Point(30, 30);
            Button.Style = "button";
            Button.Tooltip = "Close Window";
            Button.Dock = DockStyle.Right;
            Button.Margin = new Margin(0, 8, 8, 8);

            if (!allowClose)
            {
                Button.Visible = false;
                Button.Enabled = false;
            }

            Elements.Add(Button);
        }
    }
}
