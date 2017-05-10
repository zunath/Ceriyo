using System;
using Ceriyo.Core.Services.Contracts;
using Ceriyo.Server.WPF.Contracts;
using Ceriyo.Server.WPF.Factory;
using Microsoft.Xna.Framework;

namespace Ceriyo.Server.WPF
{
    public class ServerGame: Game
    {
        private readonly GraphicsDeviceManager _graphics;
        private IGameService _gameService;
        private IServerActionService _actionService;
        
        public ServerGame()
        {
            _graphics = new GraphicsDeviceManager(this)
            {
                IsFullScreen = false
            };
        }
        
        protected override void Initialize()
        {
            ServerIOCConfig.Initialize(this);
            _gameService = ServerGameFactory.GetServerGameService();
            _gameService.Initialize(_graphics);
            _actionService = ServerIOCConfig.Resolve<IServerActionService>();
            _actionService.OnExitRequestReceived += (sender, args) => Exit();

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

        public void QueueAction(IServerAction action)
        {
            _actionService.QueueAction(action);
        }

    }
}
