using Ceriyo.Core.UI.Controls;
using Squid;

namespace Ceriyo.Core.Scripting.Client.Contracts
{
    public interface IControlMethods
    {
        SampleWindow CreateWindow(int width, int height, int x, int y, string header, bool isResizeable);
        Desktop CreateNewDesktop();
        void AddWindowToDesktop(Desktop desktop, SampleWindow window);
        void ChangeDesktop(Desktop desktop);
    }
}
