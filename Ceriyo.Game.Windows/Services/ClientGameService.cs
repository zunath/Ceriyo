using Artemis;
using Ceriyo.Core.Contracts;
using Ceriyo.Core.Services.Contracts;
using Ceriyo.Game.Windows.Screens;
using Ceriyo.Infrastructure.Network.Contracts;
using Ceriyo.Infrastructure.UI.Contracts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;

namespace Ceriyo.Game.Windows.Services
{
    public class ClientGameService: IGameService
    {
        private readonly EntityWorld _world;
        private readonly SpriteBatch _spriteBatch;
        private readonly Camera2D _camera;
        private readonly IGraphicsService _graphicsService;
        private readonly IScriptService _scriptService;
        private readonly IScreenService _screenService;
        private readonly IDataService _dataService;
        private readonly IAppService _appService;
        private readonly IInputService _inputService;
        private readonly IUIService _uiService;
        private readonly IClientNetworkService _networkService;

        public ClientGameService(
            EntityWorld world,
            SpriteBatch spriteBatch,
            Camera2D camera,
            IGraphicsService graphicsService,
            IScriptService scriptService,
            IScreenService screenService,
            IDataService dataService,
            IAppService appService,
            IInputService inputService,
            IUIService uiService,
            IClientNetworkService networkService)
        {
            _world = world;
            _spriteBatch = spriteBatch;
            _camera = camera;
            _graphicsService = graphicsService;
            _scriptService = scriptService;
            _screenService = screenService;
            _dataService = dataService;
            _appService = appService;
            _inputService = inputService;
            _uiService = uiService;
            _networkService = networkService;
        }

        public void Initialize(IGraphicsDeviceManager graphics)
        {
            _appService.CreateAppDirectoryStructure();
            _dataService.Initialize();
            _graphicsService.Initialize((GraphicsDeviceManager)graphics);
            _uiService.Initialize((GraphicsDeviceManager)graphics);
            _screenService.ChangeScreen<MainMenuScreen>();
        }

        public void Update(GameTime gameTime)
        {
            _networkService.ProcessMessages();
            _inputService.Update();
            _world.Update();
            _screenService.Update();
            _uiService.Update(gameTime);
            _scriptService.ExecuteQueuedScripts();
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
