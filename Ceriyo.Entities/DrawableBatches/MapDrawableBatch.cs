using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using Ceriyo.Data;
using Ceriyo.Data.GameObjects;
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
        private int TileWidth { get; set; }
        private int TileHeight { get; set; }
        private Rectangle _sourceRectangle;

        public MapDrawableBatch(Area area)
        {
            this.TileWidth = EngineConstants.TilePixelWidth;
            this.TileHeight = EngineConstants.TilePixelHeight;
            this._sourceRectangle = new Rectangle(0, 0, TileWidth, TileHeight);

            this.DrawableArea = area;
            string path = FileManager.RelativeDirectory + @"Content/" + "Tilesets/grassland_tiles.png"; // TODO: Retrieve file from resource package
            this.MapTexture = FlatRedBallServices.Load<Texture2D>(path); 
            this.TileSprites = new SpriteList();

            int capacity = area.MapWidth * area.MapHeight;
            for (int current = 1; current <= capacity; current++)
            {
                this.TileSprites.Add(new Sprite());
            }

            foreach (Sprite sprite in TileSprites)
            {
                sprite.PixelSize = 0.5f;
                SpriteManager.AddSprite(sprite);
            }

            SpriteManager.AddDrawableBatch(this);
            SpriteManager.AddPositionedObject(this);

            LoadMap();
        }

        public void Destroy()
        {
            foreach (Sprite sprite in TileSprites)
            {
                SpriteManager.RemoveSprite(sprite);
            }

            SpriteManager.RemoveDrawableBatch(this);
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

            foreach (Tile[,] layer in DrawableArea.Tiles)
            {
                int listIndex = 0;
                int xBound = layer.GetUpperBound(0);
                int yBound = layer.GetUpperBound(1);

                for (int x = 0; x <= xBound; x++)
                {
                    for (int y = 0; y < yBound; y++)
                    {
                        Tile tile = layer[x, y];
                        Sprite sprite = TileSprites[listIndex];
                        sprite.Texture = new Texture2D(FlatRedBallServices.GraphicsDevice, TileWidth, TileHeight);
                        _sourceRectangle.X = tile.TextureCellX;
                        _sourceRectangle.Y = tile.TextureCellY;

                        Color[] data = new Color[_sourceRectangle.Width * _sourceRectangle.Height];
                        MapTexture.GetData<Color>(0, _sourceRectangle, data, 0, data.Length);
                        sprite.Texture.SetData<Color>(data);
                        sprite.Visible = tile.IsVisible;

                        sprite.X = (y * TileWidth / 2) + (x * TileWidth / 2);
                        sprite.Y = (x * TileHeight / 2) - (y * TileHeight / 2);

                        listIndex++;
                    }
                }
            }
        }

    }
}
