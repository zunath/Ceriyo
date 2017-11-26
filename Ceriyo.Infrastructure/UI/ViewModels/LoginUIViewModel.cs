using Ceriyo.Core.Contracts;
using Ceriyo.Infrastructure.UI.Contracts;
using Ceriyo.Infrastructure.UI.Generated;
using EmptyKeys.UserInterface.Generated;
using EmptyKeys.UserInterface.Input;
using EmptyKeys.UserInterface.Mvvm;
using Microsoft.Xna.Framework;

namespace Ceriyo.Infrastructure.UI.ViewModels
{
    public class LoginUIViewModel: ViewModelBase, IUIViewModel
    {
        private readonly Game _game;
        private readonly IUIService _uiService;
        private readonly IUIViewModelFactory _vmFactory;
        

        public LoginUIViewModel(Game game, IUIService uiService, IUIViewModelFactory vmFactory)
        {
            _game = game;
            _uiService = uiService;
            _vmFactory = vmFactory;
            
            LoginCommand = new RelayCommand(Login);
            CreateAccountCommand = new RelayCommand(CreateAccount);
            ExitCommand = new RelayCommand(Exit);

        }

        private string _username;

        public string Username
        {
            get => _username;
            set => SetProperty(ref _username, value);
        }

        private string _password;

        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        public ICommand LoginCommand { get; set; }

        private void Login(object obj)
        {
            // TODO: Handle login
        }

        public ICommand CreateAccountCommand { get; set; }

        private void CreateAccount(object obj)
        {
            RegisterUIViewModel vm = _vmFactory.Create<RegisterUIViewModel>();
            _uiService.ChangeUIRoot<RegisterView>(vm);
        }

        public ICommand ExitCommand { get; set; }

        private void Exit(object obj)
        {
            _game.Exit();
        }
        

    }
}
