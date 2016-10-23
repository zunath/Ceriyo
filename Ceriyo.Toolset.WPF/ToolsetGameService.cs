﻿using Artemis;
using Ceriyo.Core.Screens;
using Ceriyo.Core.Services.Contracts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Ceriyo.Toolset.WPF
{
    public class ToolsetGameService: IGameService
    {
        private readonly EntityWorld _world;
        private readonly SpriteBatch _spriteBatch;
        private readonly ICameraService _cameraService;
        private readonly IScreenService _screenService;

        public ToolsetGameService(
            EntityWorld world,
            ICameraService cameraService,
            IScreenService screenService,
            SpriteBatch spriteBatch
            )
        {
            _world = world;
            _cameraService = cameraService;
            _screenService = screenService;
            _spriteBatch = spriteBatch;
        }

        public void Initialize(IGraphicsDeviceManager graphics)
        {
            _screenService.ChangeScreen<AreaEditorScreen>();
        }

        public void Update(GameTime gameTime)
        {
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
