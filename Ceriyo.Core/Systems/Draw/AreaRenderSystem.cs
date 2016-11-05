using Artemis;
using Artemis.Attributes;
using Artemis.Manager;
using Artemis.System;
using Ceriyo.Core.Components;
using Ceriyo.Core.Constants;
using Ceriyo.Core.Services.Contracts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Ceriyo.Core.Systems.Draw
{
    [ArtemisEntitySystem(
        ExecutionType = ExecutionType.Synchronous,
        GameLoopType = GameLoopType.Draw,
        Layer = 1)]
    public class AreaRenderSystem : EntityProcessingSystem
    {
        private readonly SpriteBatch _spriteBatch;
        private readonly IEngineService _engineService;
        private readonly Vector2 _origin;
        private Texture2D _emptyCell;
        private readonly IModuleResourceService _resourceService;

        public AreaRenderSystem(SpriteBatch spriteBatch,
            IEngineService engineService,
            IModuleResourceService resourceService)
            : base(Aspect.All(typeof(Renderable),
                              typeof(Map)))
        {
            _spriteBatch = spriteBatch;
            _engineService = engineService;
            _origin = Vector2.Zero;
            _resourceService = resourceService;
        }

        public override void Process(Entity entity)
        {
            LoadEmptyTileset();

            Renderable renderable = entity.GetComponent<Renderable>();
            Map map = entity.GetComponent<Map>();

            int tileWidth = _engineService.TileWidth;
            int tileHeight = _engineService.TileHeight;

            for (int y = 0; y < map.Height; y++)
            {
                for (int x = map.Width - 1; x >= 0; x--)
                {
                    float positionX = (x * tileWidth / 2) + (y * tileWidth / 2);
                    float positionY = (y * tileHeight / 2) - (x * tileHeight / 2);

                    Vector2 position = new Vector2(
                        positionX,
                        positionY);

                    Tile tile = map.Tiles[x, y];
                    int sourceX = 0;
                    int sourceY = 0;
                    Texture2D renderTexture = _emptyCell;

                    if (tile != null)
                    {
                        sourceX = tileWidth * tile.SourceX;
                        sourceY = tileHeight * tile.SourceY;
                        renderTexture = renderable.Texture;
                    }

                    Rectangle source = new Rectangle(
                        sourceX,
                        sourceY,
                        tileWidth,
                        tileHeight);

                    _spriteBatch.Draw(
                        renderTexture,
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

        private void LoadEmptyTileset()
        {
            if (_emptyCell == null)
            {
                _emptyCell = _resourceService.LoadTexture2D(ResourceType.Tileset, "Empty-Tileset.png");
            }
        }
    }
}
