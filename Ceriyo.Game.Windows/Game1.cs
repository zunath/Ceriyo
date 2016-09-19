using Ceriyo.Core.Contracts;
using Ceriyo.Infrastructure.Factory;
using Ceriyo.Infrastructure.IOC;
using Microsoft.Xna.Framework;

namespace Ceriyo.Game.Windows
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        private readonly GraphicsDeviceManager _graphics;
        private IGameService _gameService;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
        }
        
        protected override void Initialize()
        {
            IOCConfig.Initialize(this);
            _gameService = GameFactory.GetGameService();
            _gameService.Initialize(_graphics);

            base.Initialize();
        }
        
        protected override void LoadContent()
        {
        }
        
        protected override void UnloadContent()
        {
        }
        
        protected override void Update(GameTime gameTime)
        {
            _gameService.Update(gameTime);
            base.Update(gameTime);
        }
        
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _gameService.Draw();
            base.Draw(gameTime);
        }
    }
}
