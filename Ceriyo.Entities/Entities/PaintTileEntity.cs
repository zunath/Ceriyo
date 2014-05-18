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

        public PaintTileEntity(GameResource graphic) :
            base("PaintTileEntity")
        {
            this.Processor = new GameResourceProcessor();
            this.Graphic = graphic;
            this.EntitySprite = new Sprite();
            this.EntitySprite.Visible = true;
            this.CellX = 0;
            this.CellY = 0;
            this.SourceRectangle = new Rectangle(0, 0, EngineConstants.TilePixelWidth, EngineConstants.TilePixelHeight);

            LoadEntity();
        }

        protected override void CustomInitialize()
        {
        }

        protected override void CustomActivity()
        {
            this.EntitySprite.X = InputManager.Mouse.WorldXAt(0);
            this.EntitySprite.Y = InputManager.Mouse.WorldYAt(0);
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
    }
}
