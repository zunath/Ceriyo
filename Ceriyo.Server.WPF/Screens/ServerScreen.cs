﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Artemis;
using Ceriyo.Core.Components;
using Ceriyo.Core.Constants;
using Ceriyo.Core.Contracts;
using Ceriyo.Core.Data;
using Ceriyo.Core.Entities;
using Ceriyo.Core.Services.Contracts;
using Ceriyo.Infrastructure.Network.Contracts;
using Ceriyo.Infrastructure.Network.Packets;
using Ceriyo.Infrastructure.Network.Packets.CharacterManagement;

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
        private readonly IModuleDataService _moduleDataService;
        private readonly IScriptService _scriptService;
        private readonly IDataService _dataService;
        private readonly IPathService _pathService;
        private readonly ILogger _logger;
        private readonly IServerSettingsService _settingsService;
        private Entity _gameModule;

        private Dictionary<string, Entity> _pcs;


        public ServerScreen(EntityWorld world,
            IEntityFactory entityFactory,
            IServerNetworkService networkService,
            IModuleService moduleService,
            IModuleDataService moduleDataService,
            IScriptService scriptService,
            IDataService dataService,
            IPathService pathService,
            ILogger logger,
            IServerSettingsService settingsService)
        {
            _world = world;
            _entityFactory = entityFactory;
            _networkService = networkService;
            _moduleService = moduleService;
            _moduleDataService = moduleDataService;
            _scriptService = scriptService;
            _dataService = dataService;
            _pathService = pathService;
            _logger = logger;
            _settingsService = settingsService;
        }

        public void Initialize()
        {
            _pcs = new Dictionary<string, Entity>();

            _networkService.BindPacketAction<CreateCharacterPacket>(OnCreateCharacterRequest);
            _networkService.BindPacketAction<CharacterSelectedPacket>(HandleSelectCharacterRequest);
            _networkService.BindPacketAction<DeleteCharacterPacket>(HandleDeleteCharacterRequest);
            _networkService.BindPacketAction<CharacterCreationDataPacket>(HandleCharacterCreationDataRequest);
            _networkService.OnPlayerDisconnected += PlayerDisconnected;

            LoadModule();
        }
        
        private void OnCreateCharacterRequest(string username, PacketBase p)
        {
            // TODO: Validate + sanitize packet

            CreateCharacterPacket packet = (CreateCharacterPacket) p;
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

        private void HandleSelectCharacterRequest(string username, PacketBase p)
        {
            CharacterSelectedPacket packet = (CharacterSelectedPacket) p;

            // If player is already added to the game world, we don't want to add another. Ignore this request.
            if (_pcs.ContainsKey(username))
            {
                _logger.Info($"Player {username} is already added to the game world. Ignoring request.");
                return;
            }

            string path = _pathService.ServerVaultDirectory + username + "/" + packet.PCGlobalID + ".pcf";

            if (!File.Exists(path))
            {
                _logger.Error($"PC file '{packet.PCGlobalID}.pcf' does not exist for username {username}. Cannot select character. Ignoring request.");
                return;
            }

            PCData pcData = _dataService.Load<PCData>(path);
            Entity player = _entityFactory.Create<Player, PCData>(pcData);
            
            _pcs.Add(username, player);

            ScriptGroup scripts = _gameModule.GetComponent<ScriptGroup>();
            string script = scripts[ScriptEvent.OnModulePlayerEnter];
            _scriptService.QueueScript(script, _gameModule);
            
            CharacterAddedToWorldPacket response = new CharacterAddedToWorldPacket();
            _networkService.SendMessage(PacketDeliveryMethod.ReliableUnordered, response, username);
        }

        private void HandleDeleteCharacterRequest(string username, PacketBase p)
        {
            DeleteCharacterPacket packet = (DeleteCharacterPacket) p;

            DeleteCharacterFailureType failureType = DeleteCharacterFailureType.Success;
            string path = _pathService.ServerVaultDirectory + username + "/" + packet.PCGlobalID;

            if (!File.Exists(path + ".pcf"))
            {
                _logger.Error($"PC file '{packet.PCGlobalID}' does not exist for username '{username}'. Cannot delete character. Ignoring request.");
                failureType = DeleteCharacterFailureType.FileDoesNotExist;
            }

            if (!_settingsService.AllowCharacterDeletion)
            {
                failureType = DeleteCharacterFailureType.ServerDoesNotAllowDeletion;
            }

            if (failureType == DeleteCharacterFailureType.Success)
            {
                // No hard deletes. Just rename the extension so it's not picked up by the engine.
                File.Move(path + ".pcf", path + ".dpcf");
            }

            CharacterDeletedPacket response = new CharacterDeletedPacket
            {
                PCGlobalID = packet.PCGlobalID,
                FailureType = failureType
            };

            _networkService.SendMessage(PacketDeliveryMethod.ReliableUnordered, response, username);
        }

        private void HandleCharacterCreationDataRequest(string username, PacketBase p)
        {
            CharacterCreationDataPacket response = new CharacterCreationDataPacket
            {
                Classes = _moduleDataService.LoadAll<ClassData>().ToList()
            };

            _networkService.SendMessage(PacketDeliveryMethod.ReliableUnordered, response, username);
        }

        private void PlayerDisconnected(string username)
        {
            if (_pcs.ContainsKey(username))
            {
                ScriptGroup scripts = _gameModule.GetComponent<ScriptGroup>();
                string script = scripts[ScriptEvent.OnModulePlayerLeaving];
                _scriptService.QueueScript(script, _gameModule);
                
                _pcs[username].Delete();
                _pcs.Remove(username);

                script = scripts[ScriptEvent.OnModulePlayerLeft];
                _scriptService.QueueScript(script, _gameModule);
            }
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
