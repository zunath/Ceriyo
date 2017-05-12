using System;
using System.Linq;
using Ceriyo.Core.Contracts;
using Ceriyo.Core.EventArgs;
using Ceriyo.Core.Services.Contracts;
using Ceriyo.Core.Settings;
using Ceriyo.Infrastructure.Network.Contracts;
using Ceriyo.Server.WPF.Actions;
using Ceriyo.Server.WPF.Contracts;
using Ceriyo.Server.WPF.Factory;
using Ceriyo.Server.WPF.Views.DetailsView;
using Microsoft.Xna.Framework;

namespace Ceriyo.Server.WPF
{
    public class ServerGame: Game
    {
        private readonly GraphicsDeviceManager _graphics;
        private IGameService _gameService;
        private IServerActionService _actionService;
        private readonly DetailsViewModel _ownerThreadObject;
        private IServerNetworkService _networkService;
        private readonly ServerSettings _initialSettings;
        private IServerSettingsService _settingsService;
        
        public ServerGame(DetailsViewModel ownerThread, ServerSettings initialSettings)
        {
            _ownerThreadObject = ownerThread;
            _initialSettings = initialSettings;

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

            _actionService = ServerIOCConfig.Resolve<IServerActionService>();
            

            _networkService = ServerIOCConfig.Resolve<IServerNetworkService>();
            _networkService.OnPlayerConnected += OnPlayerConnected;
            _networkService.OnPlayerDisconnected += OnPlayerDisconnected;


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

        private void OnPlayerConnected(object sender, NetworkConnectionEventArgs e)
        {
            PlayerConnectedAction action = new PlayerConnectedAction
            {
                Username = e.Username
            };
            _ownerThreadObject.QueueAction(action);
        }
        private void OnPlayerDisconnected(object sender, NetworkConnectionEventArgs e)
        {
            PlayerDisconnectedAction action = new PlayerDisconnectedAction
            {
                Username = e.Username
            };
            _ownerThreadObject.QueueAction(action);
        }

    }
}
