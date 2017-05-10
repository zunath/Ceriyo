using System;
using Artemis;
using Ceriyo.Core.Contracts;
using Ceriyo.Core.Services.Contracts;
using Ceriyo.Server.WPF.Contracts;
using Microsoft.Xna.Framework;

namespace Ceriyo.Server.WPF.Services
{
    public class ServerGameService: IGameService
    {
        private readonly EntityWorld _world;
        private readonly IScriptService _scriptService;
        private readonly IDataService _dataService;
        private readonly IAppService _appService;
        private readonly IServerActionService _actionService;

        public ServerGameService(
            EntityWorld world,
            IScriptService scriptService,
            IDataService dataService,
            IAppService appService,
            IServerActionService actionService)
        {
            _world = world;
            _scriptService = scriptService;
            _dataService = dataService;
            _appService = appService;
            _actionService = actionService;
        }

        public void Initialize(IGraphicsDeviceManager graphics)
        {
            _appService.CreateAppDirectoryStructure();
            _dataService.Initialize();
        }

        public void Update(GameTime gameTime)
        {
            _actionService.ProcessActions();
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
        }
    }
}
