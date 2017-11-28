using Ceriyo.Core.Contracts;
using Ceriyo.Infrastructure.UI.Contracts;
using EmptyKeys.UserInterface.Generated;
using EmptyKeys.UserInterface.Input;
using EmptyKeys.UserInterface.Mvvm;
using Microsoft.Xna.Framework;

namespace Ceriyo.Infrastructure.UI.ViewModels
{
    public class MainMenuUIViewModel: ViewModelBase, IUIViewModel
    {
        private readonly Game _game;
        private readonly IUIService _uiService;
        private readonly IUIViewModelFactory _vmFactory;
        
        public MainMenuUIViewModel(Game game, 
            IUIService uiService, 
            IUIViewModelFactory vmFactory,
            IUserProfile userProfile)
        {
            _game = game;
            _uiService = uiService;
            _vmFactory = vmFactory;
            
            JoinServerCommand = new RelayCommand(JoinServer);
            DirectConnectCommand = new RelayCommand(DirectConnect);
            GameSettingsCommand = new RelayCommand(GameSettings);
            ExitButtonCommand = new RelayCommand(ExitApplication);

            WelcomeText = "Welcome to Ceriyo, " + userProfile.Username + "!";
        }

        private string _welcomeText;

        public string WelcomeText
        {
            get => _welcomeText;
            set => SetProperty(ref _welcomeText, value);
        }

        public ICommand JoinServerCommand { get; set; }

        private void JoinServer(object obj)
        {
            JoinServerUIViewModel vm = _vmFactory.Create<JoinServerUIViewModel>();
            _uiService.ChangeUIRoot<JoinServerView>(vm);
        }

        public ICommand DirectConnectCommand { get; set; }

        private void DirectConnect(object obj)
        {
            DirectConnectUIViewModel vm = _vmFactory.Create<DirectConnectUIViewModel>();
            _uiService.ChangeUIRoot<DirectConnectView>(vm);
        }

        public ICommand GameSettingsCommand { get; set; }

        private void GameSettings(object obj)
        {
            SettingsUIViewModel vm = _vmFactory.Create<SettingsUIViewModel>();
            _uiService.ChangeUIRoot<SettingsView>(vm);
        }

        public ICommand ExitButtonCommand { get; set; }

        private void ExitApplication(object obj)
        {
            _game.Exit();
        }

    }
}
