using Artemis;
using Ceriyo.Core.Contracts;
using Ceriyo.Core.Screens;
using Ceriyo.Core.Services.Contracts;
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
        private readonly IUIService _uiService;

        public GameService(
            EntityWorld world,
            SpriteBatch spriteBatch,
            IGraphicsService graphicsService,
            ICameraService cameraService,
            IScriptService scriptService,
            IScreenService screenService,
            IUIService uiService)
        {
            _world = world;
            _spriteBatch = spriteBatch;
            _graphicsService = graphicsService;
            _cameraService = cameraService;
            _scriptService = scriptService;
            _screenService = screenService;
            _uiService = uiService;
        }

        public void Initialize(GraphicsDeviceManager graphics)
        {
            _graphicsService.Initialize(graphics);
            _uiService.Initialize();
            _screenService.ChangeScreen<GameScreen>();

            _scriptService.QueueScript("Client/LoadUIStyles.js", null);
            _scriptService.QueueScript("TestJS.js", null);
        }

        public void Update(GameTime gameTime)
        {
            _world.Update();
            _screenService.Update();
            _cameraService.Update();
            _scriptService.ExecuteQueuedScripts();
            _uiService.Update(gameTime);
        }

        public void Draw(GameTime gameTime)
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
            _uiService.Draw(gameTime);

            _spriteBatch.End();
        }

        public void Exit()
        {
        }
    }
}
