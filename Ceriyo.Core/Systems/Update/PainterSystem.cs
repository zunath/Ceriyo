using System;
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
            Vector2 mousePosition = _inputService.GetMousePosition();
            Position position = entity.GetComponent<Position>();
            
            int cellX = (int) ((mousePosition.X + _camera.Position.X) / _engineService.TileWidth);
            int cellY = (int) ((mousePosition.Y + _camera.Position.Y) / _engineService.TileHeight);

            position.X = cellX * _engineService.TileWidth;
            position.Y = cellY * _engineService.TileHeight;
        }
    }
}
