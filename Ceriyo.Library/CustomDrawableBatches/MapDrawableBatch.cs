﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using Ceriyo.Data.GameObjects;
using FlatRedBall;
using FlatRedBall.Graphics;
using FlatRedBall.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Ceriyo.Library.CustomDrawableBatches
{
    public class MapDrawableBatch: PositionedObject, IDrawableBatch
    {
        private Map DrawableMap { get; set; }
        private Texture2D MapTexture { get; set; }
        private SpriteList TileSprites { get; set; }
        private int TileWidth { get; set; }
        private int TileHeight { get; set; }
        private Rectangle _sourceRectangle;

        public MapDrawableBatch(Map map)
        {
            this.TileWidth = Convert.ToInt32(ConfigurationManager.AppSettings["TileWidth"]);
            this.TileHeight = Convert.ToInt32(ConfigurationManager.AppSettings["TileHeight"]);
            this._sourceRectangle = new Rectangle(0, 0, TileWidth, TileHeight);

            this.DrawableMap = map;
            this.MapTexture = FlatRedBallServices.Load<Texture2D>(FileManager.RelativeDirectory + @"Content/" + map.FilePath);
            this.TileSprites = new SpriteList();

            int capacity = map.MapWidth * map.MapHeight;
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
            foreach (Tile[,] layer in DrawableMap.MapTiles)
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
