using System;
using Artemis;
using Ceriyo.Core.Contracts;
using Ceriyo.Core.Services.Contracts;
using Ceriyo.Infrastructure.Network.Contracts;
using Ceriyo.Server.WPF.Contracts;
using Microsoft.Xna.Framework;

namespace Ceriyo.Server.WPF.Services
{
    public class ServerGameService: IGameService
    {
        private readonly EntityWorld _world;
        private readonly IServerSettingsService _settingsService;
        private readonly IScriptService _scriptService;
        private readonly IDataService _dataService;
        private readonly IAppService _appService;
        private readonly IServerActionService _actionService;
        private readonly IServerNetworkService _networkService;

        public ServerGameService(
            EntityWorld world,
            IServerSettingsService settingsService,
            IScriptService scriptService,
            IDataService dataService,
            IAppService appService,
            IServerActionService actionService,
            IServerNetworkService networkService)
        {
            _world = world;
            _settingsService = settingsService;
            _scriptService = scriptService;
            _dataService = dataService;
            _appService = appService;
            _actionService = actionService;
            _networkService = networkService;
        }

        public void Initialize(IGraphicsDeviceManager graphics)
        {
            _appService.CreateAppDirectoryStructure();
            _dataService.Initialize();
            _networkService.StartServer(_settingsService.Port);
        }

        public void Update(GameTime gameTime)
        {
            _actionService.ProcessActions();
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
