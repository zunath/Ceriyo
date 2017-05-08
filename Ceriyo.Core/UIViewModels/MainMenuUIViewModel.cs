using Ceriyo.Core.Contracts;
using EmptyKeys.UserInterface.Input;
using Microsoft.Xna.Framework;

namespace Ceriyo.Core.UIViewModels
{
    public class MainMenuUIViewModel: IUIViewModel
    {
        private readonly Game _game;

        public string JoinServerText { get; set; }

        public string ExitApplicationText { get; set; }

        public MainMenuUIViewModel(Game game)
        {
            _game = game;

            JoinServerText = "Join Server";
            ExitApplicationText = "Exit";

            ExitButtonCommand = new RelayCommand(ExitApplication);
        }

        public ICommand ExitButtonCommand { get; set; }

        private void ExitApplication(object obj)
        {
            _game.Exit();
        }

    }
}
