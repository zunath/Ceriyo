using System;
using System.Collections.Generic;
using System.Text;
using Ceriyo.Data.Enumerations;
using Squid;

namespace Ceriyo.Library.SquidGUI
{
    public class MessageBox : Dialog
    {
        private Label TitleLabel;
        private Label MessageLabel;
        private Frame ButtonFrame;

        public EventHandler<EventArgs> OnOKClicked;
        public EventHandler<EventArgs> OnCancelClicked;
        public EventHandler<EventArgs> OnRetryClicked;
        public EventHandler<EventArgs> OnYesClicked;
        public EventHandler<EventArgs> OnNoClicked;
        public EventHandler<EventArgs> OnIgnoreClicked;
        public EventHandler<EventArgs> OnAbortClicked;

        private MessageBox(string title, string message)
        {
            Modal = true; // make sure its modal
            Scissor = false;
            Padding = new Margin(7);

            TitleLabel = new Label();
            TitleLabel.Size = new Point(100, 35);
            TitleLabel.Dock = DockStyle.Top;
            TitleLabel.Text = title;
            TitleLabel.MouseDown += delegate(Control sender, MouseEventArgs args) { StartDrag(); };
            TitleLabel.MouseUp += delegate(Control sender, MouseEventArgs args) { StopDrag(); };
            TitleLabel.Cursor = Cursors.Move;
            TitleLabel.Style = "frame";
            TitleLabel.Margin = new Margin(0, 0, 0, -1);
            Controls.Add(TitleLabel);

            ButtonFrame = new Frame();
            ButtonFrame.Size = new Point(100, 35);
            ButtonFrame.Dock = DockStyle.Bottom;
            Controls.Add(ButtonFrame);

            MessageLabel = new Label();
            MessageLabel.Dock = DockStyle.Fill;
            MessageLabel.TextWrap = true;
            MessageLabel.Text = message;
            Controls.Add(MessageLabel);
        }

        public static MessageBox Show(Point size, string title, string message, MessageBoxButtonTypeEnum buttons, Desktop target)
        {
            MessageBox box = new MessageBox(title, message);
            box.Size = size;
            box.Position = (target.Size - size) / 2;
            box.InitButtons(buttons);
            box.Show(target);
            return box;
        }

        private void InitButtons(MessageBoxButtonTypeEnum buttons)
        {
            switch (buttons)
            {
                case MessageBoxButtonTypeEnum.OK:
                    AddButton("OK", DialogResult.OK, 1);
                    break;
                case MessageBoxButtonTypeEnum.OKCancel:
                    AddButton("Cancel", DialogResult.Cancel, 2);
                    AddButton("OK", DialogResult.OK, 2);
                    break;
                case MessageBoxButtonTypeEnum.RetryCancel:
                    AddButton("Cancel", DialogResult.Cancel, 2);
                    AddButton("Retry", DialogResult.Retry, 2);
                    break;
                case MessageBoxButtonTypeEnum.YesNo:
                    AddButton("No", DialogResult.No, 2);
                    AddButton("Yes", DialogResult.Yes, 2);
                    break;
                case MessageBoxButtonTypeEnum.YesNoCancel:
                    AddButton("No", DialogResult.No, 3);
                    AddButton("Cancel", DialogResult.Cancel, 3);
                    AddButton("Yes", DialogResult.Yes, 3);
                    break;
                case MessageBoxButtonTypeEnum.AbortRetryIgnore:
                    AddButton("Retry", DialogResult.Retry, 3);
                    AddButton("Ignore", DialogResult.Ignore, 3);
                    AddButton("Abort", DialogResult.Abort, 3);
                    break;
            }
        }

        private void AddButton(string text, DialogResult result, int divide)
        {
            Button button = new Button();
            button.Style = "button";
            button.Cursor = Cursors.Link;
            button.Margin = new Margin(2);
            button.Size = new Point(Size.x / (divide + 1), 35);
            button.Text = text;
            button.Tag = result;
            button.Dock = DockStyle.Right;
            button.MouseClick += button_OnMouseClick;
            ButtonFrame.Controls.Add(button);
        }

        void button_OnMouseClick(Control sender, MouseEventArgs args)
        {
            if (args.Button > 0) return;
            DialogResult result = (DialogResult)sender.Tag;

            if (result == DialogResult.Abort)
            {
                if (OnAbortClicked != null)
                {
                    OnAbortClicked(this, new EventArgs());
                }
            }
            else if (result == DialogResult.Cancel)
            {
                if (OnCancelClicked != null)
                {
                    OnCancelClicked(this, new EventArgs());
                }
            }
            else if (result == DialogResult.Ignore)
            {
                if (OnIgnoreClicked != null)
                {
                    OnIgnoreClicked(this, new EventArgs());
                }
            }
            else if (result == DialogResult.No)
            {
                if (OnNoClicked != null)
                {
                    OnNoClicked(this, new EventArgs());
                }
            }
            else if (result == DialogResult.OK)
            {
                if (OnOKClicked != null)
                {
                    OnOKClicked(this, new EventArgs());
                }
            }
            else if (result == DialogResult.Retry)
            {
                if (OnRetryClicked != null)
                {
                    OnRetryClicked(this, new EventArgs());
                }
            }
            else if (result == DialogResult.Yes)
            {
                if (OnYesClicked != null)
                {
                    OnYesClicked(this, new EventArgs());
                }
            }
            
            Close();
        }
    }
}
