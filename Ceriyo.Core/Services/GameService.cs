using Artemis;
using Ceriyo.Core.Contracts;
using Ceriyo.Core.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Ceriyo.Core.Services
{
    public class GameService: IGameService
    {
        private readonly EntityWorld _world;
        private readonly SpriteBatch _spriteBatch;
        private readonly IGraphicsService _graphicsService;
        private readonly ICameraService _cameraService;
        private readonly IScriptService _scriptService;
        private readonly IScreenService _screenService;

        public GameService(
            EntityWorld world,
            SpriteBatch spriteBatch,
            IGraphicsService graphicsService,
            ICameraService cameraService,
            IScriptService scriptService,
            IScreenService screenService
            )
        {
            _world = world;
            _spriteBatch = spriteBatch;
            _graphicsService = graphicsService;
            _cameraService = cameraService;
            _scriptService = scriptService;
            _screenService = screenService;
        }

        public void Initialize(GraphicsDeviceManager graphics)
        {
            _graphicsService.Initialize(graphics);
            _screenService.ChangeScreen<GameScreen>();
        }

        public void Update(GameTime gameTime)
        {
            _world.Update();
            _screenService.Update();
            _cameraService.Update();
            _scriptService.ExecuteQueuedScripts();
        }

        public void Draw()
        {
            _spriteBatch.Begin(SpriteSortMode.Deferred,
                BlendState.AlphaBlend,
                null,
                null,
                null,
                null,
                _cameraService.Transform);
            _screenService.Draw();
            _world.Draw();

            _spriteBatch.End();
        }

        public void Exit()
        {
        }
    }
}
