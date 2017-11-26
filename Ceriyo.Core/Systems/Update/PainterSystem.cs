using Artemis;
using Artemis.Attributes;
using Artemis.Manager;
using Artemis.System;
using Ceriyo.Core.Components;
using Ceriyo.Core.Contracts;
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
        private readonly IIsoMathService _isoMathService;
        
        public PainterSystem(IInputService inputService,
            IEngineService engineService,
            Camera2D camera,
            IIsoMathService isoMathService) 
            : base(Aspect.All(typeof(Position),
                typeof(Paintable)))
        {
            _inputService = inputService;
            _camera = camera;
            _engineService = engineService;
            _isoMathService = isoMathService;
        }
        
        public override void Process(Entity entity)
        {
            Vector2 mousePosition = _inputService.GetMousePosition();
            Position position = entity.GetComponent<Position>();
            Paintable paintable = entity.GetComponent<Paintable>();

            mousePosition.X += _camera.Position.X;
            mousePosition.Y += _camera.Position.Y;
            
            Vector2 tilePosition = _isoMathService.ScreenPositionToMapTile(mousePosition);
            Vector2 worldPosition = _isoMathService.MapTileToScreenPosition(tilePosition);
            
            position.X = worldPosition.X;
            position.Y = worldPosition.Y;


            if (_inputService.IsLeftMouseDown())
            {
                
            }

        }

    }
}
