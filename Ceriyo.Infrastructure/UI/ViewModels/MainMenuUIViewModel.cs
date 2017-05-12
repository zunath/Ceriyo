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

        public string JoinServerText { get; set; }

        public string DirectConnectText { get; set; }

        public string SettingsText { get; set; }

        public string ExitApplicationText { get; set; }

        public MainMenuUIViewModel(Game game, IUIService uiService, IUIViewModelFactory vmFactory)
        {
            _game = game;
            _uiService = uiService;
            _vmFactory = vmFactory;

            JoinServerText = "Join Server";
            DirectConnectText = "Direct Connect";
            SettingsText = "Settings";
            ExitApplicationText = "Exit";

            JoinServerCommand = new RelayCommand(JoinServer);
            DirectConnectCommand = new RelayCommand(DirectConnect);
            SettingsCommand = new RelayCommand(Settings);
            ExitButtonCommand = new RelayCommand(ExitApplication);

        }

        public ICommand JoinServerCommand { get; set; }

        private void JoinServer(object obj)
        {
            
        }

        public ICommand DirectConnectCommand { get; set; }

        private void DirectConnect(object obj)
        {
            DirectConnectUIViewModel vm = _vmFactory.Create<DirectConnectUIViewModel>();
            _uiService.ChangeUIRoot<DirectConnect>(vm);
        }

        public ICommand SettingsCommand { get; set; }

        private void Settings(object obj)
        {
            
        }

        public ICommand ExitButtonCommand { get; set; }

        private void ExitApplication(object obj)
        {
            _game.Exit();
        }

    }
}
