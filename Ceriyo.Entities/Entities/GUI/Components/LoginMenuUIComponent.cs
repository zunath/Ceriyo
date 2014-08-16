using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ceriyo.Entities.GUI;
using Squid;

namespace Ceriyo.Entities.Entities.GUI.Components
{
    public class LoginMenuUIComponent
    {
        public LoginMenuUIComponent(Desktop parent)
        {
            UIWindow window1 = new UIWindow();
            window1.Size = new Squid.Point(440, 340);
            window1.Position = new Squid.Point(40, 40);
            window1.Titlebar.Text = "Log In";
            window1.Resizable = false;
            window1.Parent = parent;

            Label label1 = new Label();
            label1.Text = "Username:";
            label1.Size = new Squid.Point(122, 35);
            label1.Position = new Squid.Point(60, 100);
            label1.Parent = window1;

            TextBox textbox1 = new TextBox { Name = "textbox" };
            textbox1.Text = string.Empty;
            textbox1.Size = new Squid.Point(222, 35);
            textbox1.Position = new Squid.Point(180, 100);
            textbox1.Style = "textbox";
            textbox1.Parent = window1;
            textbox1.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right;

            Label label2 = new Label();
            label2.Text = "Password:";
            label2.Size = new Squid.Point(122, 35);
            label2.Position = new Squid.Point(60, 140);
            label2.Parent = window1;

            TextBox textbox2 = new TextBox { Name = "textbox" };
            textbox2.PasswordChar = '*';
            textbox2.IsPassword = true;
            textbox2.Text = string.Empty;
            textbox2.Size = new Squid.Point(222, 35);
            textbox2.Position = new Squid.Point(180, 140);
            textbox2.Style = "textbox";
            textbox2.Parent = window1;
            textbox2.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right;

            Button button = new Button();
            button.Size = new Squid.Point(157, 35);
            button.Position = new Squid.Point(437 - 192, 346 - 52);
            button.Text = "Log In";
            button.Style = "button";
            button.Parent = window1;
            button.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            button.Cursor = Cursors.Link;

            CheckBox box = new CheckBox();
            box.Size = new Squid.Point(157, 26);
            box.Position = new Squid.Point(280, 180);
            box.Text = "Remember Me";
            box.Parent = window1;
            box.Button.Style = "checkBox";
            box.Button.Size = new Squid.Point(26, 26);
            box.Button.Cursor = Cursors.Link;
        }
    }
}
