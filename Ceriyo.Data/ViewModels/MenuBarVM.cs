
namespace Ceriyo.Data.ViewModels
{
    public class MenuBarVM : BaseVM
    {
        private bool _isModuleLoaded;
        private bool _isAreaLoaded;

        public bool IsModuleLoaded
        {
            get
            {
                return _isModuleLoaded;
            }
            set
            {
                _isModuleLoaded = value;
                OnPropertyChanged("IsModuleLoaded");
            }
        }

        public bool IsAreaLoaded
        {
            get
            {
                return _isAreaLoaded;
            }
            set
            {
                _isAreaLoaded = value;
                OnPropertyChanged("IsAreaLoaded");
            }
        }

        public MenuBarVM()
        {
            IsModuleLoaded = false;
            IsAreaLoaded = false;
        }
    }
}
