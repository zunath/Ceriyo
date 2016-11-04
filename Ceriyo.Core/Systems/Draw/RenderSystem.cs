using Artemis;
using Artemis.Attributes;
using Artemis.Manager;
using Artemis.System;
using Ceriyo.Core.Components;
using Ceriyo.Core.Constants;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;

namespace Ceriyo.Core.Systems.Draw
{
    [ArtemisEntitySystem(
        ExecutionType = ExecutionType.Synchronous,
        GameLoopType = GameLoopType.Draw,
        Layer = 1)]
    public class RenderSystem: EntityProcessingSystem
    {
        private readonly SpriteBatch _spriteBatch;
        private readonly Camera2D _camera;

        public RenderSystem(SpriteBatch spriteBatch,
            Camera2D camera) 
            : base(Aspect.All(typeof(Renderable),
                typeof(Position)))
        {
            _spriteBatch = spriteBatch;
            _camera = camera;
        }

        public override void Process(Entity entity)
        {
            Renderable renderable = entity.GetComponent<Renderable>();
            Position position = entity.GetComponent<Position>();
            Vector2 origin = new Vector2((int)(renderable.Source.Width / 2), (int)(renderable.Source.Height / 2));
            
            _spriteBatch.Draw(renderable.Texture,                   // Texture
                new Vector2(position.X, position.Y),                // Position
                renderable.Source,                                  // Source
                renderable.ColorOverride ?? Color.White,            // Color
                0.0f,                                               // Rotation
                origin,                                             // Origin 
                1.0f,                                               // Scale
                SpriteEffects.None,                                 // SpriteEffects
                0.0f);                                              // Layer Depth
        }
    }
}
