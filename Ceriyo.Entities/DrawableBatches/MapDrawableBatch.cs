using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Ceriyo.Data;
using Ceriyo.Data.EventArguments;
using Ceriyo.Data.GameObjects;
using Ceriyo.Library.Processing;
using FlatRedBall;
using FlatRedBall.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Ceriyo.Entities.DrawableBatches
{
    public class MapDrawableBatch: PositionedObject, IDrawableBatch
    {
        private Area DrawableArea { get; set; }
        private Texture2D MapTexture { get; set; }
        private Rectangle _sourceRectangle;
        private GameResourceProcessor Processor { get; set; }
        private Texture2D EmptyTileTexture { get; set; }

        public BindingList<MapTile> MapTiles
        {
            get
            {
                return DrawableArea.MapTiles;
            }
        }

        public MapDrawableBatch(Area area)
        {
            Processor = new GameResourceProcessor();
            _sourceRectangle = new Rectangle(0, 0, EngineConstants.TilePixelWidth, EngineConstants.TilePixelHeight);

            DrawableArea = area;
            MapTexture = Processor.ToTexture2D(area.AreaTileset.Graphic);


            foreach (MapTile tile in MapTiles)
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
            ClearSprites();
            SpriteManager.RemovePositionedObject(this);
        }

        private void ClearSprites()
        {
            foreach (MapTile tile in MapTiles)
            {
                SpriteManager.RemoveSprite(tile.TileSprite);
            }
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
            MapTile tile = MapTiles.SingleOrDefault(t => t.Layer == e.Layer && 
                                                         t.MapX == e.StartCellX && 
                                                         t.MapY == e.StartCellY);

            if (tile != null)
            {
                tile.TileDefinitionX = e.StartTextureCellX;
                tile.TileDefinitionY = e.StartTextureCellY;
                tile.HasGraphic = true;

                tile.TileSprite.Texture = e.Texture;
            }
        }

        private void LoadMap()
        {
            List<MapTile> orderedTiles = MapTiles
                .OrderBy(l => l.Layer)
                .ThenBy(x => x.MapX)
                .ThenBy(y => y.MapY).ToList();

            int listIndex = 0;
            foreach (MapTile tile in orderedTiles)
            {
                MapTiles[listIndex].MapX = tile.MapX;
                MapTiles[listIndex].MapY = tile.MapY;
                MapTiles[listIndex].Layer = tile.Layer;
                MapTiles[listIndex].TileDefinitionX = tile.TileDefinitionX;
                MapTiles[listIndex].TileDefinitionY = tile.TileDefinitionY;

                Sprite sprite = MapTiles[listIndex].TileSprite;

                if (tile.HasGraphic)
                {
                    TileDefinition definition = DrawableArea.AreaTileset.Tiles
                        .SingleOrDefault(t => t.TextureCellX == tile.TileDefinitionX &&
                                              t.TextureCellY == tile.TileDefinitionY);

                    if (definition != null)
                    {
                        sprite.Texture = new Texture2D(FlatRedBallServices.GraphicsDevice, EngineConstants.TilePixelWidth, EngineConstants.TilePixelHeight);
                        _sourceRectangle.X = definition.TextureCellX * EngineConstants.TilePixelWidth;
                        _sourceRectangle.Y = definition.TextureCellY * EngineConstants.TilePixelHeight;

                        Color[] data = new Color[_sourceRectangle.Width * _sourceRectangle.Height];
                        MapTexture.GetData(0, _sourceRectangle, data, 0, data.Length);
                        sprite.Texture.SetData(data);
                        sprite.Visible = true;
                    }
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

        public void AreaPropertiesSaved(object sender, AreaPropertiesChangedEventArgs e)
        {
            // Area modified is currently open and its dimensions have changed.
            if(e.ModifiedArea.Resref == DrawableArea.Resref && 
               (e.ModifiedArea.MapWidth != DrawableArea.MapWidth ||
               e.ModifiedArea.MapHeight != DrawableArea.MapHeight))
            {
                //ClearSprites();
                //DrawableArea = e.ModifiedArea;

                //LoadMap();
            }
        }

    }
}
