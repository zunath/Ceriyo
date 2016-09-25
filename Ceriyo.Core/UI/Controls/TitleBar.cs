﻿using Squid;

namespace Ceriyo.Core.UI.Controls
{
    public class TitleBar : Label
    {
        public Button Button { get; private set; }

        public TitleBar()
        {
            Button = new Button
            {
                Size = new Point(30, 30),
                Style = "button",
                Tooltip = "Close Window",
                Dock = DockStyle.Right,
                Margin = new Margin(0, 8, 8, 8)
            };
            Elements.Add(Button);
        }
    }
}
