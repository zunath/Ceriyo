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
        private readonly IDataService _dataService;
        private readonly IPathService _pathService;
        private Entity _gameModule;

        public ServerScreen(EntityWorld world,
            IEntityFactory entityFactory,
            IServerNetworkService networkService,
            IModuleService moduleService,
            IScriptService scriptService,
            IDataService dataService,
            IPathService pathService)
        {
            _world = world;
            _entityFactory = entityFactory;
            _networkService = networkService;
            _moduleService = moduleService;
            _scriptService = scriptService;
            _dataService = dataService;
            _pathService = pathService;
        }

        public void Initialize()
        {
            _networkService.OnPacketReceived += PacketReceived;

            LoadModule();
        }

        private void PacketReceived(string username, PacketBase p)
        {
            if (p.GetType() == typeof(CreateCharacterPacket))
            {
                var packet = (CreateCharacterPacket) p;
                HandleCreateCharacterRequest(username, packet);
            }
        }

        private void HandleCreateCharacterRequest(string username, CreateCharacterPacket packet)
        {
            // TODO: Validate + sanitize packet

            PCData pcData = new PCData
            {
                LastName = packet.LastName,
                FirstName = packet.FirstName
            };

            string path = _pathService.ServerVaultDirectory + username + "/" + pcData.GlobalID + ".pcf";
            _dataService.Save(pcData, path);

            CharacterCreatedPacket response = new CharacterCreatedPacket
            {
                Description = pcData.Description,
                LastName = pcData.LastName,
                FirstName = pcData.FirstName,
                Level = pcData.Level,
                GlobalID = pcData.GlobalID
            };

            _networkService.SendMessage(PacketDeliveryMethod.ReliableUnordered, response, username);
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
