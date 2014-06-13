using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ceriyo.Data;
using Ceriyo.Data.EventArguments;
using Ceriyo.Data.ResourceObjects;
using Ceriyo.Library.Processing;
using FlatRedBall;
using FlatRedBall.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Ceriyo.Entities.Entities
{
    public class PaintTileEntity : BaseEntity
    {
        private GameResourceProcessor Processor { get; set; }
        private GameResource Graphic { get; set; }
        private Sprite EntitySprite { get; set; }
        private Rectangle SourceRectangle { get; set; }
        private int AreaWidth { get; set; }
        private int AreaHeight { get; set; }

        public int CellX
        {
            get;
            private set;
        }

        public int CellY
        {
            get;
            private set;
        }

        public PaintTileEntity(GameResource graphic, int areaWidth, int areaHeight) :
            base("PaintTileEntity")
        {
            this.Processor = new GameResourceProcessor();
            this.Graphic = graphic;
            this.EntitySprite = new Sprite();
            this.EntitySprite.Visible = true;
            this.EntitySprite.Z = EngineConstants.AreaMaxLayers + 1;
            this.CellX = 0;
            this.CellY = 0;
            this.SourceRectangle = new Rectangle(0, 0, EngineConstants.TilePixelWidth, EngineConstants.TilePixelHeight);
            this.AreaWidth = areaWidth;
            this.AreaHeight = areaHeight;

            LoadEntity();
        }

        protected override void CustomInitialize()
        {
        }

        protected override void CustomActivity()
        {
            if (InputManager.Mouse.IsInGameWindow())
            {
                CellX = Convert.ToInt32(InputManager.Mouse.WorldXAt(0) / EngineConstants.TilePixelWidth);
                CellY = Convert.ToInt32(InputManager.Mouse.WorldYAt(0) / EngineConstants.TilePixelHeight);

                this.X = CellX * (EngineConstants.TilePixelWidth);
                this.Y = CellY * (EngineConstants.TilePixelHeight);
                CheckBounds();
                
                
            }
        }

        protected override void CustomDestroy()
        {
            SpriteManager.RemoveSprite(EntitySprite);
        }

        public void LoadEntity()
        {
            EntitySprite.PixelSize = 0.5f;
            EntitySprite.Texture = Processor.GetSubTexture(Graphic,
                CellX * EngineConstants.TilePixelWidth,
                CellY * EngineConstants.TilePixelHeight,
                EngineConstants.TilePixelWidth,
                EngineConstants.TilePixelHeight);

            EntitySprite.AttachTo(this, false);
            SpriteManager.AddSprite(EntitySprite);
        }

        public void PaintTile(object sender, TilePaintEventArgs e)
        {

        }

        public void SetTilesetCoordinates(int cellX, int cellY)
        {
            this.CellX = cellX;
            this.CellY = cellY;
            EntitySprite.Texture = Processor.GetSubTexture(Graphic, 
                CellX * EngineConstants.TilePixelWidth, 
                CellY * EngineConstants.TilePixelHeight, 
                EngineConstants.TilePixelWidth, 
                EngineConstants.TilePixelHeight);

        }

        private void CheckBounds()
        {
            if (this.X < 0)
            {
                this.X = 0;
            }
            if (this.Y < 0)
            {
                this.Y = 0;
            }

            if (this.X > (AreaWidth - 1) * EngineConstants.TilePixelWidth)
            {
                this.X = (AreaWidth - 1) * EngineConstants.TilePixelWidth;
            }
            if (this.Y > (AreaHeight - 1) * EngineConstants.TilePixelHeight)
            {
                this.Y = (AreaHeight - 1) * EngineConstants.TilePixelHeight;
            }
        }
    }
}
