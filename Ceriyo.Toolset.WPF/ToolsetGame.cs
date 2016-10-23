using Ceriyo.Core.Services.Contracts;
using Ceriyo.Infrastructure.WPF.MonoGameWpfInterop;
using Ceriyo.Infrastructure.WPF.MonoGameWpfInterop.Input;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Xna.Framework;

namespace Ceriyo.Toolset.WPF
{
    public class ToolsetGame: WpfGame
    {
        private readonly WpfGraphicsDeviceService _graphics;
        private IGameService _gameService;
        private WpfKeyboard _keyboard;
        private WpfMouse _mouse;

        public ToolsetGame()
        {
            _graphics = new WpfGraphicsDeviceService(this);
            Content.RootDirectory = "Compiled";
        }

        protected override void Initialize()
        {
            ToolsetIOCConfig.RegisterGraphicsDevice(GraphicsDevice);
            _gameService = ServiceLocator.Current.TryResolve<IGameService>();
            _keyboard = new WpfKeyboard(this);
            _mouse = new WpfMouse(this);
            _gameService.Initialize(null);

            base.Initialize();
        }

        protected override void Update(GameTime gameTime)
        {
            // every update we can now query the keyboard & mouse for our WpfGame
            var mouseState = _mouse.GetState();
            var keyboardState = _keyboard.GetState();
            _gameService.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.LightGray);
            _gameService.Draw(gameTime);
            base.Draw(gameTime);
        }
    }
}
