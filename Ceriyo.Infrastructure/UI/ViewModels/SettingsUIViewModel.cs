using Ceriyo.Core.Contracts;
using Ceriyo.Infrastructure.UI.Contracts;
using Ceriyo.Infrastructure.UI.Generated;
using EmptyKeys.UserInterface.Generated;
using EmptyKeys.UserInterface.Input;
using EmptyKeys.UserInterface.Mvvm;

namespace Ceriyo.Infrastructure.UI.ViewModels
{
    public class SettingsUIViewModel: ViewModelBase, IUIViewModel
    {

        private readonly IUIService _uiService;
        private readonly IUIViewModelFactory _vmFactory;

        public SettingsUIViewModel(IUIService uiService,
            IUIViewModelFactory vmFactory)
        {
            _uiService = uiService;
            _vmFactory = vmFactory;

            BackCommand = new RelayCommand(Back);
            SaveCommand = new RelayCommand(Save);
        }

        public ICommand BackCommand { get; set; }

        private void Back(object obj)
        {
            MainMenuUIViewModel vm = _vmFactory.Create<MainMenuUIViewModel>();
            _uiService.ChangeUIRoot<MainMenuView>(vm);
        }

        public ICommand SaveCommand { get; set; }

        private void Save(object obj)
        {
            
        }
    }
}
