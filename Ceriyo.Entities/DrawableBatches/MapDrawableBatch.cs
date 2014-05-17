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
        private Rectangle _sourceRectangle;

        public MapDrawableBatch(Area area)
        {
            GameResourceProcessor processor = new GameResourceProcessor();

            this._sourceRectangle = new Rectangle(0, 0, EngineConstants.TilePixelWidth, EngineConstants.TilePixelHeight);

            this.DrawableArea = area;
            byte[] imageData = processor.ToBytes(area.AreaTileset.Graphic);
            this.MapTexture = Texture2D.FromStream(FlatRedBallServices.GraphicsDevice, new MemoryStream(imageData));
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

            SpriteManager.AddPositionedObject(this);

            LoadMap();
        }

        public void Destroy()
        {
            foreach (Sprite sprite in TileSprites)
            {
                SpriteManager.RemoveSprite(sprite);
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

        private void LoadMap()
        {
            List<MapTile> orderedTiles = DrawableArea.MapTiles
                .OrderBy(l => l.Layer)
                .ThenBy(x => x.MapX)
                .ThenBy(y => y.MapY).ToList();

            int listIndex = 0;
            foreach (MapTile tile in orderedTiles)
            {
                TileDefinition definition = tile.Definition;
                Sprite sprite = TileSprites[listIndex];
                sprite.Texture = new Texture2D(FlatRedBallServices.GraphicsDevice, EngineConstants.TilePixelWidth, EngineConstants.TilePixelHeight);
                _sourceRectangle.X = definition.TextureCellX;
                _sourceRectangle.Y = definition.TextureCellY;

                Color[] data = new Color[_sourceRectangle.Width * _sourceRectangle.Height];
                MapTexture.GetData<Color>(0, _sourceRectangle, data, 0, data.Length);
                sprite.Texture.SetData<Color>(data);
                sprite.Visible = tile.IsVisible;

                sprite.X = (tile.MapY * EngineConstants.TilePixelWidth / 2) + (tile.MapX * EngineConstants.TilePixelWidth / 2);
                sprite.Y = (tile.MapX * EngineConstants.TilePixelHeight / 2) - (tile.MapY * EngineConstants.TilePixelHeight / 2);

                listIndex++;
            }
        }

    }
}
