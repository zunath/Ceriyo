using System;
using Squid;

namespace Ceriyo.Core.UI.Controls
{
    public class ChatBox : Control
    {
        public TextBox Input { get; private set; }
        public Label Output { get; private set; }
        public ScrollBar Scrollbar { get; private set; }
        public Frame Frame { get; private set; }

        public ChatBox()
        {
            Size = new Point(100, 100);

            Input = new TextBox();
            Input.Size = new Point(100, 35);
            Input.Dock = DockStyle.Bottom;
            Input.TextCommit += Input_OnTextCommit;
            Elements.Add(Input);

            Scrollbar = new ScrollBar();
            Scrollbar.Dock = DockStyle.Right;
            Scrollbar.Size = new Point(25, 25);
            Elements.Add(Scrollbar);

            Frame = new Frame();
            Frame.Dock = DockStyle.Fill;
            Frame.Scissor = true;
            Elements.Add(Frame);

            Output = new Label();
            Output.BBCodeEnabled = true;
            Output.TextWrap = true;
            Output.AutoSize = Squid.AutoSize.Vertical;
            Output.Text = "Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet. Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet.";
            Output.Style = "multiline";
            Frame.Controls.Add(Output);
        }

        void Input_OnTextCommit(object sender, EventArgs e)
        {
            Append(Input.Text); // try to append the text
            Input.Text = string.Empty;
            Input.Focus();
        }

        protected override void OnUpdate()
        {
            // force the width to be that of its parent
            Output.Size = new Point(Frame.Size.x, Output.Size.y);

            // move the label up/down using the scrollbar value
            if (Output.Size.y < Frame.Size.y) // no need to scroll
            {
                Scrollbar.Visible = false; // hide scrollbar
                Output.Position = new Point(0, Frame.Size.y - Output.Size.y); // set fixed position
            }
            else
            {
                Scrollbar.Scale = Math.Min(1, (float)Frame.Size.y / (float)Output.Size.y);
                Scrollbar.Visible = true; // show scrollbar
                Output.Position = new Point(0, (int)((Frame.Size.y - Output.Size.y) * Scrollbar.Value));
            }

            // the mouse is scrolling and there is any control hovered
            if (GuiHost.MouseScroll != 0 && Desktop.HotControl != null)
            {
                // ok, lets check if the mouse is anywhere near us
                if (Hit(GuiHost.MousePosition.x, GuiHost.MousePosition.y))
                {
                    // now lets check if its really this window or anything in it
                    if (Desktop.HotControl == this || Desktop.HotControl.IsChildOf(this))
                        Scrollbar.Scroll(GuiHost.MouseScroll);
                }
            }
        }

        public void Append(string text)
        {
            // check for null/empty
            if (string.IsNullOrEmpty(text))
                return;

            // return if only whitespaces were entered
            if (text.Trim().Length == 0)
                return;

            string prefix = ""; // "[Username]: ";

            if (string.IsNullOrEmpty(Output.Text))
                Output.Text = prefix + text;
            else
                Output.Text += Environment.NewLine + prefix + text;

            Scrollbar.Value = 1; // scroll down
        }
    }
}
