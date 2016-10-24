using Artemis;
using Ceriyo.Core.Contracts;
using Ceriyo.Core.Services.Contracts;
using Ceriyo.Game.Windows.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;

namespace Ceriyo.Game.Windows.Services
{
    public class GameService: IGameService
    {
        private readonly EntityWorld _world;
        private readonly SpriteBatch _spriteBatch;
        private readonly Camera2D _camera;
        private readonly IGraphicsService _graphicsService;
        private readonly IScriptService _scriptService;
        private readonly IScreenService _screenService;
        private readonly IUIService _uiService;
        private readonly IDataService _dataService;
        private readonly IAppService _appService;
        private readonly IInputService _inputService;

        public GameService(
            EntityWorld world,
            SpriteBatch spriteBatch,
            Camera2D camera,
            IGraphicsService graphicsService,
            IScriptService scriptService,
            IScreenService screenService,
            IUIService uiService,
            IDataService dataService,
            IAppService appService,
            IInputService inputService)
        {
            _world = world;
            _spriteBatch = spriteBatch;
            _camera = camera;
            _graphicsService = graphicsService;
            _scriptService = scriptService;
            _screenService = screenService;
            _uiService = uiService;
            _dataService = dataService;
            _appService = appService;
            _inputService = inputService;
        }

        public void Initialize(IGraphicsDeviceManager graphics)
        {
            _appService.CreateAppDirectoryStructure();
            _dataService.Initialize();
            _graphicsService.Initialize((GraphicsDeviceManager)graphics);
            _uiService.Initialize();
            _screenService.ChangeScreen<GameScreen>();

            _scriptService.QueueScript("Client/LoadUIStyles.js", null);
            _scriptService.QueueScript("TestJS.js", null);
        }

        public void Update(GameTime gameTime)
        {
            _inputService.Update();
            _world.Update();
            _screenService.Update();
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
                _camera.GetViewMatrix());
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
