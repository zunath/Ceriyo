using System;
using Artemis;
using Artemis.Attributes;
using Artemis.Manager;
using Artemis.System;
using Ceriyo.Core.Components;
using Ceriyo.Core.Constants;
using Ceriyo.Core.Services.Contracts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
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
            Paintable paintable = entity.GetComponent<Paintable>();

            mousePosition.X += _camera.Position.X;
            mousePosition.Y += _camera.Position.Y;
            

            float halfWidth = _engineService.TileWidth / 2.0f;
            float halfHeight = _engineService.TileHeight / 2.0f;
            
            float x = (mousePosition.X / halfWidth + mousePosition.Y / halfHeight) / 2;
            float y = (mousePosition.Y / halfHeight - (mousePosition.X / halfWidth)) / 2;
            
            position.X = ((int)x - (int)y) * _engineService.TileWidth / 2.0f;
            position.Y = ((int)x + (int)y) * _engineService.TileHeight / 2.0f;
            
        }

    }
}
