
namespace Ceriyo.Data.ViewModels
{
    public class HotBarVM: BaseVM
    {
        private bool _isAreaLoaded;
        private bool _isModuleLoaded;

        public bool IsAreaLoaded
        {
            get { return _isAreaLoaded; }
            set
            {
                _isAreaLoaded = value;
                OnPropertyChanged("IsAreaLoaded");
            }
        }

        public bool IsModuleLoaded
        {
            get { return _isModuleLoaded; }
            set
            {
                _isModuleLoaded = value;
                OnPropertyChanged("IsModuleLoaded");
            }
        }

        public HotBarVM()
        {
            IsAreaLoaded = false;
            IsModuleLoaded = false;
        }
    }
}
