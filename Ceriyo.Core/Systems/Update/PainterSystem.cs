using Artemis;
using Artemis.Attributes;
using Artemis.Manager;
using Artemis.System;
using Ceriyo.Core.Components;
using Ceriyo.Core.Services.Contracts;
using Microsoft.Xna.Framework;
using MonoGame.Extended;

namespace Ceriyo.Core.Systems.Update
{
    [ArtemisEntitySystem(
        ExecutionType = ExecutionType.Synchronous,
        GameLoopType = GameLoopType.Update,
        Layer = 1)]
    public class PainterSystem: EntityProcessingSystem
    {
        private readonly Camera2D _camera;
        private readonly IInputService _inputService;
        private readonly IEngineService _engineService;

        public PainterSystem(IInputService inputService,
            IEngineService engineService,
            Camera2D camera) 
            : base(Aspect.All(typeof(Position),
                typeof(Paintable)))
        {
            _inputService = inputService;
            _camera = camera;
            _engineService = engineService;
        }

        public override void Process(Entity entity)
        {
            Paintable paintable = entity.GetComponent<Paintable>();
            Vector2 mousePosition = _inputService.GetMousePosition();
            Position position = entity.GetComponent<Position>();
            
            int tileWidth = _engineService.TileWidth;
            int tileHeight = _engineService.TileHeight;

            //mousePosition = _camera.ScreenToWorld(mousePosition);
            int x = (int)mousePosition.X / tileWidth;
            int y = (int)mousePosition.Y / tileHeight;

            float positionX = (x * tileWidth / 2) + (y * tileWidth / 2);
            float positionY = (y * tileHeight / 2) - (x * tileHeight / 2);

            position.X = positionX;
            position.Y = positionY;

        }
    }
}
