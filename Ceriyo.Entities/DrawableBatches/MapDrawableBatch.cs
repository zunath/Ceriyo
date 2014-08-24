using System.Collections.Generic;
using System.Linq;
using Ceriyo.Data;
using Ceriyo.Data.EventArguments;
using Ceriyo.Data.GameObjects;
using Ceriyo.Library.Processing;
using FlatRedBall;
using FlatRedBall.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Ceriyo.Entities
{
    public class MapDrawableBatch: PositionedObject, IDrawableBatch
    {
        private Area DrawableArea { get; set; }
        private Texture2D MapTexture { get; set; }
        private List<MapTile> DrawableTiles { get; set; }
        private Rectangle _sourceRectangle;
        private GameResourceProcessor Processor { get; set; }
        private Texture2D EmptyTileTexture { get; set; }

        public MapDrawableBatch(Area area)
        {
            this.Processor = new GameResourceProcessor();
            this._sourceRectangle = new Rectangle(0, 0, EngineConstants.TilePixelWidth, EngineConstants.TilePixelHeight);

            this.DrawableArea = area;
            this.MapTexture = Processor.ToTexture2D(area.AreaTileset.Graphic);
            this.DrawableTiles = new List<MapTile>();

            int capacity = area.MapWidth * area.MapHeight * area.LayerCount;
            for (int current = 1; current <= capacity; current++)
            {
                this.DrawableTiles.Add(new MapTile());
            }

            foreach (MapTile tile in DrawableTiles)
            {
                tile.TileSprite.PixelSize = 0.5f;
                SpriteManager.AddSprite(tile.TileSprite);
            }

            SpriteManager.AddPositionedObject(this);

            EmptyTileTexture = FlatRedBallServices.Load<Texture2D>("Content/Tilesets/emptytile.png");
            LoadMap();
        }

        public void Destroy()
        {
            foreach (MapTile tile in DrawableTiles)
            {
                SpriteManager.RemoveSprite(tile.TileSprite);
            }
            SpriteManager.RemovePositionedObject(this);
        }

        public void Draw(Camera camera)
        {
        }

        public void Update()
        {
            
        }

        public bool UpdateEveryFrame
        {
            get { return false; }
        }

        public void PaintTile(object sender, TilePaintEventArgs e)
        {
            MapTile tile = DrawableTiles.SingleOrDefault(t => t.Layer == e.Layer && 
                                                                   t.MapX == e.StartCellX && 
                                                                   t.MapY == e.StartCellY);

            tile.TileDefinitionX = e.StartTextureCellX;
            tile.TileDefinitionY = e.StartTextureCellY;
            

            if (tile != null)
            {
                tile.TileSprite.Texture = e.Texture;
            }
        }

        private void LoadMap()
        {
            List<MapTile> orderedTiles = DrawableArea.MapTiles
                .OrderBy(l => l.Layer)
                .ThenBy(x => x.MapX)
                .ThenBy(y => y.MapY).ToList();

            int listIndex = 0;
            foreach (MapTile tile in orderedTiles)
            {
                DrawableTiles[listIndex].MapX = tile.MapX;
                DrawableTiles[listIndex].MapY = tile.MapY;
                DrawableTiles[listIndex].Layer = tile.Layer;
                DrawableTiles[listIndex].TileDefinitionX = tile.TileDefinitionX;
                DrawableTiles[listIndex].TileDefinitionY = tile.TileDefinitionY;

                Sprite sprite = DrawableTiles[listIndex].TileSprite;

                if (tile.HasGraphic)
                {
                    TileDefinition definition = DrawableArea.AreaTileset.Tiles
                        .SingleOrDefault(t => t.TextureCellX == tile.TileDefinitionX &&
                                              t.TextureCellY == tile.TileDefinitionY);

                    sprite.Texture = new Texture2D(FlatRedBallServices.GraphicsDevice, EngineConstants.TilePixelWidth, EngineConstants.TilePixelHeight);
                    _sourceRectangle.X = definition.TextureCellX * EngineConstants.TilePixelWidth;
                    _sourceRectangle.Y = definition.TextureCellY * EngineConstants.TilePixelHeight;

                    Color[] data = new Color[_sourceRectangle.Width * _sourceRectangle.Height];
                    MapTexture.GetData<Color>(0, _sourceRectangle, data, 0, data.Length);
                    sprite.Texture.SetData<Color>(data);
                    sprite.Visible = true;
                }
                else if(!tile.HasGraphic && 
                         tile.Layer == 0)
                {
                    sprite.Texture = EmptyTileTexture;
                }

                sprite.X = tile.MapX * EngineConstants.TilePixelWidth;
                sprite.Y = tile.MapY * EngineConstants.TilePixelHeight;

                listIndex++;
            }
        }

        public List<MapTile> GetMapTiles()
        {
            List<MapTile> mapTiles = new List<MapTile>();

            mapTiles = (from tile
                        in DrawableTiles
                        select new MapTile
                        {
                            Layer = tile.Layer,
                            MapX = tile.MapX,
                            MapY = tile.MapY,
                            TileDefinitionX = tile.TileDefinitionX,
                            TileDefinitionY = tile.TileDefinitionY

                        }).ToList();

            return mapTiles;
        }
    }
}
