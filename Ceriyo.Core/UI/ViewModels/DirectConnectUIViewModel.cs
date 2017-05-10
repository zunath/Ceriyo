using Ceriyo.Core.Contracts;
using Ceriyo.Core.Services.Contracts;
using EmptyKeys.UserInterface.Generated;
using EmptyKeys.UserInterface.Input;
using EmptyKeys.UserInterface.Mvvm;

namespace Ceriyo.Core.UI.ViewModels
{
    public class DirectConnectUIViewModel: ViewModelBase, IUIViewModel
    {
        private readonly IUIService _uiService;
        private readonly IUIViewModelFactory _vmFactory;

        public string IPAddress { get; set; }
        public string Password { get; set; }


        public DirectConnectUIViewModel(IUIService uiService,
            IUIViewModelFactory vmFactory)
        {
            _uiService = uiService;
            _vmFactory = vmFactory;

            BackCommand = new RelayCommand(Back);
            ConnectCommand = new RelayCommand(Connect);

            // DEBUGGING

            IPAddress = "127.0.0.1"; // localhost for testing

            // END DEBUGGING

        }

        public ICommand BackCommand { get; set; }

        private void Back(object obj)
        {
            MainMenuUIViewModel vm = _vmFactory.Create<MainMenuUIViewModel>();
            _uiService.ChangeUIRoot<MainMenu>(vm);
        }

        public ICommand ConnectCommand { get; set; }

        private void Connect(object obj)
        {
            
        }

    }
}
