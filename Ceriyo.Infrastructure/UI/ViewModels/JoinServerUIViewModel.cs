using Ceriyo.Core.Contracts;
using Ceriyo.Infrastructure.UI.Contracts;
using EmptyKeys.UserInterface.Generated;
using EmptyKeys.UserInterface.Input;
using EmptyKeys.UserInterface.Mvvm;

namespace Ceriyo.Infrastructure.UI.ViewModels
{
    public class JoinServerUIViewModel: ViewModelBase, IUIViewModel
    {
        private readonly IUIService _uiService;
        private readonly IUIViewModelFactory _vmFactory;


        public JoinServerUIViewModel(IUIService uiService,
            IUIViewModelFactory vmFactory)
        {
            _uiService = uiService;
            _vmFactory = vmFactory;

            BackCommand = new RelayCommand(Back);
            JoinServerCommand = new RelayCommand(JoinServer);
        }

        public ICommand BackCommand { get; set; }

        private void Back(object obj)
        {
            MainMenuUIViewModel vm = _vmFactory.Create<MainMenuUIViewModel>();
            _uiService.ChangeUIRoot<MainMenuView>(vm);
        }

        public ICommand JoinServerCommand { get; set; }

        private void JoinServer(object obj)
        {
            
        }

    }
}
