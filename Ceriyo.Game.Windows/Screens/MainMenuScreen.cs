using Ceriyo.Core.Contracts;
using Ceriyo.Infrastructure.UI.Contracts;
using Ceriyo.Infrastructure.UI.ViewModels;
using EmptyKeys.UserInterface.Generated;

namespace Ceriyo.Game.Windows.Screens
{
    public class MainMenuScreen: IScreen
    {
        private readonly IUIViewModelFactory _uiViewModelFactory;
        private readonly IUIService _uiService;

        public MainMenuScreen(IUIViewModelFactory viewModelFactory,
            IUIService uiService)
        {
            _uiViewModelFactory = viewModelFactory;
            _uiService = uiService;
        }

        public void Initialize()
        {
            MainMenuUIViewModel vm = _uiViewModelFactory.Create<MainMenuUIViewModel>();
            _uiService.ChangeUIRoot<MainMenuView>(vm);
        }

        public void Update()
        {

        }

        public void Draw()
        {

        }

        public void Close()
        {

        }
    }
}
