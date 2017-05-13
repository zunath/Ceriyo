using System;
using Artemis;
using Ceriyo.Core.Contracts;
using Ceriyo.Core.Services.Contracts;
using Ceriyo.Infrastructure.Network.Contracts;
using Ceriyo.Server.WPF.Screens;
using Microsoft.Xna.Framework;

namespace Ceriyo.Server.WPF.Services
{
    /// <summary>
    /// ServerGameService is responsible for initializing and cleaning up
    /// all app-wide services. It should not handle game specific logic. 
    /// That should be handled in the ServerScreen class.
    /// </summary>
    public class ServerGameService: IGameService
    {
        private readonly EntityWorld _world;
        private readonly IServerSettingsService _settingsService;
        private readonly IScriptService _scriptService;
        private readonly IDataService _dataService;
        private readonly IAppService _appService;
        private readonly IServerNetworkService _networkService;
        private readonly IScreenService _screenService;
        private readonly IObjectMapper _objectMapper;
        private readonly ISystemLoader _systemLoader;

        public ServerGameService(
            EntityWorld world,
            IServerSettingsService settingsService,
            IScriptService scriptService,
            IDataService dataService,
            IAppService appService,
            IServerNetworkService networkService,
            IScreenService screenService,
            IObjectMapper objectMapper,
            ISystemLoader systemLoader)
        {
            _world = world;
            _settingsService = settingsService;
            _scriptService = scriptService;
            _dataService = dataService;
            _appService = appService;
            _networkService = networkService;
            _screenService = screenService;
            _objectMapper = objectMapper;
            _systemLoader = systemLoader;
        }

        public void Initialize(IGraphicsDeviceManager graphics)
        {
            _objectMapper.Initialize();
            _appService.CreateAppDirectoryStructure();
            _dataService.Initialize();
            _networkService.StartServer(_settingsService.Port);
            _screenService.ChangeScreen<ServerScreen>();
            _systemLoader.LoadSystems();
        }

        public void Update(GameTime gameTime)
        {
            _networkService.ProcessMessages();
            _world.Update();
            _scriptService.ExecuteQueuedScripts();
        }

        public void Draw(GameTime gameTime)
        {
            // Do not add logic. Server does not draw.
            throw new NotSupportedException("Server cannot draw.");
        }

        public void Exit()
        {
            _networkService.StopServer();
        }

    }
}
