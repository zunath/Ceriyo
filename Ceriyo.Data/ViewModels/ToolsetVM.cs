using Ceriyo.Data.GameObjects;

namespace Ceriyo.Data.ViewModels
{
    public class ToolsetVM : BaseVM
    {
        private string _windowTitle;

        public string WindowTitle
        {
            get
            {
                return _windowTitle;
            }
            set
            {
                _windowTitle = value;
                OnPropertyChanged("WindowTitle");
            }
        }

        public ToolsetVM()
        {
            WindowTitle = "Ceriyo Editor";
        }

    }
}
