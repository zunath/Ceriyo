using System;
using Artemis;
using Artemis.Attributes;
using Artemis.Manager;
using Artemis.System;
using Ceriyo.Core.Components;
using Ceriyo.Core.Constants;
using Ceriyo.Core.Contracts;
using Ceriyo.Core.Services.Contracts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;

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
        private readonly Camera2D _camera;
        private readonly IIsoMathService _isoMathService;

        public AreaRenderSystem(SpriteBatch spriteBatch,
            IEngineService engineService,
            IModuleResourceService resourceService,
            Camera2D camera,
            IIsoMathService isoMathService)
            : base(Aspect.All(typeof(Renderable),
                              typeof(Map)))
        {
            _spriteBatch = spriteBatch;
            _engineService = engineService;
            _origin = Vector2.Zero;
            _resourceService = resourceService;
            _camera = camera;
            _isoMathService = isoMathService;
        }

        private bool _renderedOnce;
        public override void Process(Entity entity)
        {
            LoadEmptyTileset();

            Renderable renderable = entity.GetComponent<Renderable>();
            Map map = entity.GetComponent<Map>();

            int tileWidth = _engineService.TileWidth;
            int tileHeight = _engineService.TileHeight;

            for (int x = 0; x < map.Width; x++)
            {
                for (int y = map.Height - 1; y >= 0; y--)
                {
                    Vector2 screenPosition = _isoMathService.MapTileToScreenPosition(x, y);
                    
                    Vector2 position = new Vector2(
                        screenPosition.X,
                        screenPosition.Y);

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
            
            // Move camera to focus more on the area for the first run time.
            if (!_renderedOnce)
            {
                _camera.Position = new Vector2(
                    _camera.Position.X - _camera.BoundingRectangle.Width / 2.0f,
                    _camera.Position.Y - 50
                );

            }

            _renderedOnce = true;
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
