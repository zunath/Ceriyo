using Squid;

namespace Ceriyo.Core.UI.Controls
{
    public class TitledWindow : Window
    {
        public TitleBar Titlebar { get; }

        public TitledWindow()
        {
            AllowDragOut = true;
            Padding = new Margin(4);

            Titlebar = new TitleBar
            {
                Dock = DockStyle.Top,
                Size = new Point(122, 35)
            };
            Titlebar.MouseDown += delegate { StartDrag(); };
            Titlebar.MouseUp += delegate { StopDrag(); };
            Titlebar.Cursor = Cursors.Move;
            Titlebar.Style = "frame";
            Titlebar.Margin = new Margin(-4, -4, -4, -1);
            Titlebar.Button.MouseClick += Button_OnMouseClick;
            Titlebar.TextAlign = Alignment.MiddleLeft;
            Titlebar.BBCodeEnabled = true;
            AllowDragOut = false;

            Controls.Add(Titlebar);
        }

        void Button_OnMouseClick(Control sender, MouseEventArgs args)
        {
            Animation.Custom(FadeAndClose());
        }

        private System.Collections.IEnumerator FadeAndClose()
        {
            yield return Animation.Opacity(0, 500);
            Close();
        }
    }

    
}
