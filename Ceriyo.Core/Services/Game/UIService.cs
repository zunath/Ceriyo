using System;
using Ceriyo.Core.Services.Contracts;
using EmptyKeys.UserInterface;
using EmptyKeys.UserInterface.Controls;
using EmptyKeys.UserInterface.Generated;
using Microsoft.Xna.Framework;

namespace Ceriyo.Core.Services.Game
{
    public class UIService: IUIService
    {
        private readonly IGraphicsService _graphicsService;
        private MonoGameEngine _uiEngine;
        private UIRoot _activeRoot;
        private readonly Microsoft.Xna.Framework.Game _game;

        public UIService(
            Microsoft.Xna.Framework.Game game,
            IGraphicsService graphicsService)
        {
            _game = game;
            _graphicsService = graphicsService;
        }

        public void Initialize(IGraphicsDeviceManager graphics)
        {
            _uiEngine = new MonoGameEngine(
                _graphicsService.GraphicsDevice,
                _graphicsService.GraphicsDevice.Viewport.Width,
                _graphicsService.GraphicsDevice.Viewport.Height);

            _game.Window.ClientSizeChanged += WindowOnClientSizeChanged;

            // DEBUGGING

            ChangeUIRoot<MainMenu>();

            // END DEBUGGING
        }

        private void WindowOnClientSizeChanged(object sender, EventArgs eventArgs)
        {
            _activeRoot?.Resize(
                _graphicsService.GraphicsDevice.Viewport.Width,
                _graphicsService.GraphicsDevice.Viewport.Height);
        }

        public void Update(GameTime gameTime)
        {
            if (_activeRoot == null) return;
            _activeRoot.UpdateInput(gameTime.ElapsedGameTime.TotalMilliseconds);
            _activeRoot.UpdateLayout(gameTime.ElapsedGameTime.TotalMilliseconds);
        }

        public void Draw(GameTime gameTime)
        {
            _activeRoot?.Draw(gameTime.ElapsedGameTime.TotalMilliseconds);
        }

        public void Exit()
        {
        }

        public void ChangeUIRoot<T>() 
            where T : UIRoot
        {
            UIRoot root = Activator.CreateInstance<T>();
            _activeRoot = root;
        }
    }
}
