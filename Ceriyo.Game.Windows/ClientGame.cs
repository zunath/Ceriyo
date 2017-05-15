using System;
using Ceriyo.Core.Services.Contracts;
using Ceriyo.Game.Windows.Factory;
using Microsoft.Xna.Framework;

namespace Ceriyo.Game.Windows
{
    public class ClientGame : Microsoft.Xna.Framework.Game
    {
        private readonly GraphicsDeviceManager _graphics;
        private IGameService _gameService;

        private int _backupWidth;
        private int _backupHeight;

        public ClientGame()
        {
            _graphics = new GraphicsDeviceManager(this)
            {
                SynchronizeWithVerticalRetrace = false
            };
            Window.AllowUserResizing = true;
            Window.ClientSizeChanged += WindowOnClientSizeChanged;
            Content.RootDirectory = "Compiled";
            IsMouseVisible = true;
        }

        private void WindowOnClientSizeChanged(object sender, EventArgs eventArgs)
        {
            // Workaround for minimizing window.
            if (Window.ClientBounds.Width == 0 && Window.ClientBounds.Height == 0)
            {
                _graphics.PreferredBackBufferWidth = _backupWidth;
                _graphics.PreferredBackBufferHeight = _backupHeight;
            }
            else
            {
                _graphics.PreferredBackBufferWidth = Window.ClientBounds.Width;
                _graphics.PreferredBackBufferHeight = Window.ClientBounds.Height;
            }

            
        }

        protected override void Initialize()
        {
            ClientGameIOCConfig.Initialize(this);
            _gameService = ClientGameFactory.GetClientGameService();
            _gameService.Initialize(_graphics);

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
            _backupHeight = _graphics.PreferredBackBufferHeight;
            _backupWidth = _graphics.PreferredBackBufferWidth;

            _gameService.Update(gameTime);
            base.Update(gameTime);
            _graphics.ApplyChanges();
        }
        
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            _gameService.Draw(gameTime);
            base.Draw(gameTime);
        }

        protected override void OnExiting(object sender, EventArgs args)
        {
            _gameService.Exit();
            base.OnExiting(sender, args);
        }
    }
}
