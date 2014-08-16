using System;
using System.Collections.Generic;
using System.Text;
using Ceriyo.Entities.Entities.GUI.Components;
using Squid;

namespace Ceriyo.Entities.GUI
{
    public class UIWindow : Window
    {
        public TitleBarUIComponent Titlebar { get; private set; }

        public UIWindow(bool canDragWindow = false, bool canCloseWindow = false)
        {
            AllowDragOut = true;
            Padding = new Margin(4);

            Titlebar = new TitleBarUIComponent(canCloseWindow);
            Titlebar.Dock = DockStyle.Top;
            Titlebar.Size = new Squid.Point(122, 35);

            if (canDragWindow)
            {
                Titlebar.MouseDown += delegate(Control sender, MouseEventArgs args) { StartDrag(); };
                Titlebar.MouseUp += delegate(Control sender, MouseEventArgs args) { StopDrag(); };
                Titlebar.Cursor = Cursors.Move;
            }

            Titlebar.Style = "frame";
            Titlebar.Margin = new Margin(-4, -4, -4, -1);
            Titlebar.Button.MouseClick += Button_OnMouseClick;
            Titlebar.TextAlign = Alignment.MiddleLeft;
            Titlebar.BBCodeEnabled = true;

            Controls.Add(Titlebar);

            AllowDragOut = false;
        }

        private void Button_OnMouseClick(Control sender, MouseEventArgs args)
        {
            Close();
        }
    }
}
