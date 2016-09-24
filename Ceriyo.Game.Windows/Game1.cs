using System;
using Ceriyo.Core.Contracts;
using Ceriyo.Game.Windows.UI;
using Ceriyo.Infrastructure.Factory;
using Ceriyo.Infrastructure.IOC;
using Ceriyo.Infrastructure.UI;
using Microsoft.Xna.Framework;
using Squid;

namespace Ceriyo.Game.Windows
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        private readonly GraphicsDeviceManager _graphics;
        private IGameService _gameService;

        private int _backupWidth;
        private int _backupHeight;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this)
            {
                SynchronizeWithVerticalRetrace = false
            };
            Window.AllowUserResizing = true;
            Window.ClientSizeChanged += WindowOnClientSizeChanged;
            Content.RootDirectory = "Compiled";
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
            IOCConfig.InitializeGame(this);
            _gameService = GameFactory.GetGameService();
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
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _gameService.Draw(gameTime);
            base.Draw(gameTime);
        }
    }
}
