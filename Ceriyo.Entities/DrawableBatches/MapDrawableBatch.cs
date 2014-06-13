using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using Ceriyo.Data;
using Ceriyo.Data.GameObjects;
using Ceriyo.Library.Processing;
using FlatRedBall;
using FlatRedBall.Graphics;
using FlatRedBall.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Ceriyo.Entities
{
    public class MapDrawableBatch: PositionedObject, IDrawableBatch
    {
        private Area DrawableArea { get; set; }
        private Texture2D MapTexture { get; set; }
        private SpriteList TileSprites { get; set; }
        private Sprite SelectedTile { get; set; }
        private int SelectedCellX { get; set; }
        private int SelectedCellY { get; set; }
        private Rectangle _sourceRectangle;
        private GameResourceProcessor Processor { get; set; }

        public MapDrawableBatch(Area area)
        {
            this.Processor = new GameResourceProcessor();
            this.SelectedTile = new Sprite();
            this._sourceRectangle = new Rectangle(0, 0, EngineConstants.TilePixelWidth, EngineConstants.TilePixelHeight);

            this.DrawableArea = area;
            this.MapTexture = Processor.ToTexture2D(area.AreaTileset.Graphic);
            this.TileSprites = new SpriteList();

            int capacity = area.MapWidth * area.MapHeight * area.LayerCount;
            for (int current = 1; current <= capacity; current++)
            {
                this.TileSprites.Add(new Sprite());
            }

            foreach (Sprite sprite in TileSprites)
            {
                sprite.PixelSize = 0.5f;
                SpriteManager.AddSprite(sprite);
            }

            SpriteManager.AddSprite(SelectedTile);
            SpriteManager.AddPositionedObject(this);
            

            LoadMap();
        }

        public void Destroy()
        {
            SpriteManager.RemoveSpriteList(TileSprites);
            SpriteManager.RemoveSprite(SelectedTile);
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

        private void LoadMap()
        {
            Texture2D emptyTexture = FlatRedBallServices.Load<Texture2D>("Content/Tilesets/emptytile.png");
            List<MapTile> orderedTiles = DrawableArea.MapTiles
                .OrderBy(l => l.Layer)
                .ThenBy(x => x.MapX)
                .ThenBy(y => y.MapY).ToList();

            int listIndex = 0;
            foreach (MapTile tile in orderedTiles)
            {
                Sprite sprite = TileSprites[listIndex];

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
                    sprite.Texture = emptyTexture;
                }

                sprite.X = tile.MapX * EngineConstants.TilePixelWidth;
                sprite.Y = tile.MapY * EngineConstants.TilePixelHeight;

                listIndex++;
            }
        }

    }
}
