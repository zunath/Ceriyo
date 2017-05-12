using System;
using Ceriyo.Core.Services.Contracts;
using EmptyKeys.UserInterface;
using EmptyKeys.UserInterface.Controls;
using EmptyKeys.UserInterface.Media;
using EmptyKeys.UserInterface.Media.Effects;
using EmptyKeys.UserInterface.Mvvm;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Ceriyo.Core.Services.Game
{
    public class UIService: IUIService
    {
        private readonly IGraphicsService _graphicsService;
        private MonoGameEngine _uiEngine;
        private UIRoot _activeRoot;
        private readonly Microsoft.Xna.Framework.Game _game;
        private readonly ContentManager _contentManager;
        
        public object ActiveViewModel { get; private set; }

        public UIService(
            Microsoft.Xna.Framework.Game game,
            IGraphicsService graphicsService)
        {
            _game = game;
            _graphicsService = graphicsService;
            _contentManager = game.Content;
        }

        public void Initialize(IGraphicsDeviceManager graphics)
        {
            _uiEngine = new MonoGameEngine(
                _graphicsService.GraphicsDevice,
                _graphicsService.GraphicsDevice.Viewport.Width,
                _graphicsService.GraphicsDevice.Viewport.Height);

            SpriteFont font = _contentManager.Load<SpriteFont>("Spritefonts/Segoe_UI_10_Regular");
            FontManager.DefaultFont = _uiEngine.Renderer.CreateFont(font); 

            FontManager.Instance.LoadFonts(_contentManager);
            ImageManager.Instance.LoadImages(_contentManager);
            SoundManager.Instance.LoadSounds(_contentManager);
            EffectManager.Instance.LoadEffects(_contentManager);

            _game.Window.ClientSizeChanged += WindowOnClientSizeChanged;
            
        }

        private void WindowOnClientSizeChanged(object sender, System.EventArgs eventArgs)
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

        public void ChangeUIRoot<T>(ViewModelBase viewModel) 
            where T : UIRoot
        {
            UIRoot root = Activator.CreateInstance<T>();
            _activeRoot = root;
            
            ActiveViewModel = viewModel;
            _activeRoot.DataContext = viewModel;
        }
    }
}
