using Ceriyo.Core.Contracts;
using Ceriyo.Core.Scripting.Client.Contracts;
using Ceriyo.Core.Services.Contracts;
using Ceriyo.Core.UI.Controls;
using Squid;

namespace Ceriyo.Core.Scripting.Client
{
    public class ControlMethods: IControlMethods
    {
        private readonly IUIService _uiService;

        public ControlMethods(IUIService uiService)
        {
            _uiService = uiService;
        }

        public SampleWindow CreateWindow(
            int width,
            int height,
            int x,
            int y,
            string header,
            bool isResizeable
            )
        {
            SampleWindow window = new SampleWindow
            {
                Size = new Point(width, height),
                Position = new Point(x, y),
                Titlebar = {Text = header},
                Resizable = isResizeable
            };

            return window;
        }

        public Desktop CreateNewDesktop()
        {
            return new Desktop();
        }

        public void AddWindowToDesktop(Desktop desktop, SampleWindow window)
        {
            window.Parent = desktop;
        }

        public void ChangeDesktop(Desktop desktop)
        {
            _uiService.ChangeDesktop(desktop);
        }
    }
}
