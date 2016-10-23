using Artemis;
using Ceriyo.Core.Services.Contracts;
using Ceriyo.Toolset.WPF.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Ceriyo.Toolset.WPF.GameWorld
{
    public class ToolsetGameService: IGameService
    {
        private readonly EntityWorld _world;
        private readonly SpriteBatch _spriteBatch;
        private readonly ICameraService _cameraService;
        private readonly IScreenService _screenService;
        private readonly IInputService _inputService;

        public ToolsetGameService(
            EntityWorld world,
            ICameraService cameraService,
            IScreenService screenService,
            SpriteBatch spriteBatch,
            IInputService inputService)
        {
            _world = world;
            _cameraService = cameraService;
            _screenService = screenService;
            _spriteBatch = spriteBatch;
            _inputService = inputService;
        }

        public void Initialize(IGraphicsDeviceManager graphics)
        {
            _screenService.ChangeScreen<AreaEditorScreen>();
        }

        public void Update(GameTime gameTime)
        {
            _inputService.Update();
            _world.Update();
            _screenService.Update();
            _cameraService.Update();
        }

        public void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin(SpriteSortMode.Deferred,
                BlendState.AlphaBlend,
                null,
                null,
                null,
                null,
                _cameraService.Transform);
            _screenService.Draw();
            _world.Draw();

            _spriteBatch.End();
        }

        public void Exit()
        {

        }
    }
}
