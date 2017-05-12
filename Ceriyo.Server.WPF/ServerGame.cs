using System;
using System.Linq;
using Ceriyo.Core.Contracts;
using Ceriyo.Core.EventArgs;
using Ceriyo.Core.Services.Contracts;
using Ceriyo.Core.Settings;
using Ceriyo.Infrastructure.Network.Contracts;
using Ceriyo.Server.WPF.Factory;
using Microsoft.Xna.Framework;

namespace Ceriyo.Server.WPF
{
    public class ServerGame: Game
    {
        private readonly GraphicsDeviceManager _graphics;
        private IGameService _gameService;
        private IServerNetworkService _networkService;
        private readonly ServerSettings _initialSettings;
        private IServerSettingsService _settingsService;
        private readonly string _moduleName;
        private IModuleService _moduleService;

        public event Action<string> OnPlayerConnected;
        public event Action<string> OnPlayerDisconnected;

        public ServerGame(ServerSettings initialSettings, string moduleName)
        {
            _initialSettings = initialSettings;
            _moduleName = moduleName;

            _graphics = new GraphicsDeviceManager(this)
            {
                IsFullScreen = false
            };
        }


        protected override void Initialize()
        {
            ServerIOCConfig.Initialize(this);

            // Any data from the GUI thread needed for the logic thread 
            // needs to be copied here.
            _settingsService = ServerIOCConfig.Resolve<IServerSettingsService>();
            _settingsService.CopySettings(_initialSettings);

            _gameService = ServerGameFactory.GetServerGameService();
            _gameService.Initialize(_graphics);
            
            _networkService = ServerIOCConfig.Resolve<IServerNetworkService>();
            _networkService.OnPlayerConnected += PlayerConnected;
            _networkService.OnPlayerDisconnected += PlayerDisconnected;

            _moduleService = ServerIOCConfig.Resolve<IModuleService>();
            _moduleService.OpenModule(_moduleName);

            base.Initialize();
        }


        protected override void LoadContent()
        {
            
        }

        protected override void UnloadContent()
        {
            
        }

        protected override void Update(GameTime gameTime)
        {
            _gameService.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            SuppressDraw();
        }
        
        protected override void OnExiting(object sender, EventArgs args)
        {
            _gameService.Exit();
            _moduleService.CloseModule();
            
            base.OnExiting(sender, args);
        }
        
        public void BanUsername(string username)
        {
            _networkService.BootUsername(username);

            if (_settingsService.BlackList.Contains(username)) return;

            _settingsService.BlackList.Add(username);
        }
        
        public void BootUsername(string username)
        {
            _networkService.BootUsername(username);
        }

        public void SendServerMessage(string message)
        {
            
        }

        public void RefreshSettings(ServerSettings settings)
        {
            _settingsService.CopySettings(settings);
        }
        
        private void PlayerConnected(object sender, NetworkConnectionEventArgs e)
        {
            OnPlayerConnected?.Invoke(e.Username);
        }
        private void PlayerDisconnected(object sender, NetworkConnectionEventArgs e)
        {
            OnPlayerDisconnected?.Invoke(e.Username);
        }

    }
}
