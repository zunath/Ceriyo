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
        Layer = 2)]
    public class PainterSystem: EntityProcessingSystem
    {
        private readonly Camera2D _camera;
        private readonly IInputService _inputService;

        public PainterSystem(IInputService inputService,
            Camera2D camera) 
            : base(Aspect.All(typeof(Position),
                typeof(Paintable)))
        {
            _inputService = inputService;
            _camera = camera;
        }

        public override void Process(Entity entity)
        {
            Vector2 mousePosition = _camera.ScreenToWorld(_inputService.GetMousePosition());
            Position position = entity.GetComponent<Position>();
            position.X = mousePosition.X;
            position.Y = mousePosition.Y;

            
        }
    }
}
