using Artemis;
using Artemis.Attributes;
using Artemis.Manager;
using Artemis.System;
using Ceriyo.Core.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Ceriyo.Core.Systems.Draw
{
    [ArtemisEntitySystem(
        ExecutionType = ExecutionType.Synchronous,
        GameLoopType = GameLoopType.Draw,
        Layer = 1)]
    public class RenderSystem: EntityProcessingSystem
    {
        private readonly SpriteBatch _spriteBatch;

        public RenderSystem(SpriteBatch spriteBatch) 
            : base(Aspect.All(typeof(Renderable),
                typeof(Position)))
        {
            _spriteBatch = spriteBatch;
        }

        public override void Process(Entity entity)
        {
            Renderable renderable = entity.GetComponent<Renderable>();
            Position position = entity.GetComponent<Position>();

            _spriteBatch.Draw(renderable.Texture,                   // Texture
                new Vector2(position.X, position.Y),                // Position
                renderable.Source,                                  // Source
                renderable.ColorOverride ?? Color.White,            // Color
                0.0f,                                               // Rotation
                renderable.Origin,                                  // Origin 
                1.0f,                                               // Scale
                SpriteEffects.None,                                 // SpriteEffects
                0.0f);                                              // Layer Depth
        }
    }
}
