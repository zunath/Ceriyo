using Ceriyo.Core.Contracts;
using Ceriyo.Core.Services.Contracts;
using Ceriyo.Infrastructure.WPF.MonoGameWpfInterop;
using Ceriyo.Infrastructure.WPF.MonoGameWpfInterop.Input;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Ceriyo.Toolset.WPF.GameWorld
{
    public class ToolsetGame: WpfGame
    {
        private readonly WpfGraphicsDeviceService _graphics;
        private IGameService _gameService;
        

        public ToolsetGame(GraphicsDevice graphics)
            :base(graphics)
        {
            _graphics = new WpfGraphicsDeviceService(this);

            Content.RootDirectory = "Compiled";
        }

        protected override void Initialize()
        {
            _gameService = ServiceLocator.Current.TryResolve<IGameService>();
            _gameService.Initialize(null);

            base.Initialize();
        }

        protected override void Update(GameTime gameTime)
        {
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
