using System;
using Artemis;
using Ceriyo.Core.Components;
using Ceriyo.Core.Constants;
using Ceriyo.Core.Contracts;
using Ceriyo.Core.Data;
using Ceriyo.Core.Entities;
using Ceriyo.Core.Services.Contracts;
using Ceriyo.Infrastructure.Network.Contracts;
using Ceriyo.Infrastructure.Network.Packets;

namespace Ceriyo.Server.WPF.Screens
{
    /// <summary>
    /// ServerScreen is responsible for managing game specific state.
    /// Game specific logic should be placed here.
    /// </summary>
    public class ServerScreen: IScreen
    {
        private EntityWorld _world;
        private readonly IEntityFactory _entityFactory;
        private readonly IServerNetworkService _networkService;
        private readonly IModuleService _moduleService;
        private readonly IScriptService _scriptService;
        private Entity _gameModule;

        public ServerScreen(EntityWorld world,
            IEntityFactory entityFactory,
            IServerNetworkService networkService,
            IModuleService moduleService,
            IScriptService scriptService)
        {
            _world = world;
            _entityFactory = entityFactory;
            _networkService = networkService;
            _moduleService = moduleService;
            _scriptService = scriptService;
        }

        public void Initialize()
        {
            _networkService.OnPacketReceived += PacketReceived;

            LoadModule();
        }

        private void PacketReceived(PacketBase packetBase)
        {

        }

        public void Update()
        {
        }

        public void Draw()
        {
            throw new NotSupportedException("Server cannot draw.");
        }

        public void Close()
        {
        }

        private void LoadModule()
        {
            ModuleData modData = _moduleService.GetLoadedModuleData();
            _gameModule = _entityFactory.Create<Module, ModuleData>(modData);

            LoadAreas();

            // Mod load script should be run last - after everything else has loaded.
            ScriptGroup scripts = _gameModule.GetComponent<ScriptGroup>();
            string modLoadScript = scripts[ScriptEvent.OnModuleLoad];
            if (!string.IsNullOrWhiteSpace(modLoadScript))
            {
                _scriptService.QueueScript(modLoadScript, _gameModule);
            }
        }

        private void LoadAreas()
        {
            
        }

    }
}
