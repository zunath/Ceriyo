using Artemis;
using Artemis.Attributes;
using Artemis.Manager;
using Artemis.System;
using Ceriyo.Core.Components;
using Ceriyo.Core.Services.Contracts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Ceriyo.Core.Systems
{
    [ArtemisEntitySystem(
        ExecutionType = ExecutionType.Synchronous,
        GameLoopType = GameLoopType.Draw,
        Layer = 1)]
    public class AreaRenderSystem: EntityProcessingSystem
    {
        private readonly SpriteBatch _spriteBatch;
        private readonly IEngineService _engineService;
        private readonly Vector2 _origin;

        private const int TileStepX = 64;
        private const int TileStepY = 16;
        private const int OddRowXOffset = 32;
        private const int HeightTileOffset = 32;


        public AreaRenderSystem(SpriteBatch spriteBatch,
            IEngineService engineService) 
            : base(Aspect.All(typeof(Renderable),
                              typeof(Map)))
        {
            _spriteBatch = spriteBatch;
            _engineService = engineService;
            _origin = Vector2.Zero;
        }

        public override void Process(Entity entity)
        {
            Renderable renderable = entity.GetComponent<Renderable>();
            Map map = entity.GetComponent<Map>();

            int tileWidth = _engineService.TileWidth;
            int tileHeight = _engineService.TileHeight;

            int sourceX = (tileWidth*0);   
            int sourceY = (tileHeight*1); 

            for (int y = 0; y < map.Height; y++)
            {
                for (int x = map.Width; x >= 0; x--)
                {
                    float positionX = (x * tileWidth / 2) + (y * tileWidth / 2);
                    float positionY = (y * tileHeight / 2) - (x * tileHeight / 2);

                    Vector2 position = new Vector2(
                        positionX,
                        positionY);

                    Rectangle source = new Rectangle(
                        sourceX,
                        sourceY,
                        tileWidth,
                        tileHeight);

                    _spriteBatch.Draw(
                        renderable.Texture,
                        position,
                        source,
                        Color.White,
                        0.0f,
                        _origin,
                        1.0f,
                        SpriteEffects.None, 
                        0.0f);

                }
            }

        }
    }
}
