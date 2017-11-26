using Ceriyo.Core.Contracts;
using Ceriyo.Infrastructure.UI.Contracts;
using Ceriyo.Infrastructure.UI.Generated;
using Ceriyo.Infrastructure.UI.ViewModels;
using EmptyKeys.UserInterface.Generated;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Ceriyo.Game.Windows.Screens
{
    public class GameScreen: IScreen
    {
        private readonly GraphicsDevice _graphics;
        private readonly IUIService _uiService;
        private readonly IUIViewModelFactory _vmFactory;

        public GameScreen(IUIService uiService,
            IUIViewModelFactory vmFactory,
            GraphicsDevice graphics)
        {
            _uiService = uiService;
            _vmFactory = vmFactory;
            _graphics = graphics;
        }

        public void Initialize()
        {
            GameUIViewModel vm = _vmFactory.Create<GameUIViewModel>();
            _uiService.ChangeUIRoot<GameView>(vm);

        }

        public void Update()
        {
            
        }

        public void Draw()
        {
            _graphics.Clear(Color.CornflowerBlue);
        }

        public void Close()
        {
            
        }
    }
}
