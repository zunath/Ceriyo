using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ceriyo.Data;
using Ceriyo.Data.EventArguments;
using Ceriyo.Data.ResourceObjects;
using Ceriyo.Library.Processing;
using FlatRedBall;
using FlatRedBall.Gui;
using FlatRedBall.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Ceriyo.Entities
{
    public class PaintTileEntity : BaseEntity
    {
        private GameResourceProcessor Processor { get; set; }
        private GameResource Graphic { get; set; }
        private Sprite EntitySprite { get; set; }
        private Rectangle SourceRectangle { get; set; }
        private int AreaWidth { get; set; }
        private int AreaHeight { get; set; }
        private int Layer { get; set; }
        public event EventHandler<TilePaintEventArgs> OnTilePainted;
        private int StartCellX { get; set; }
        private int StartCellY { get; set; }
        private int EndCellX { get; set; }
        private int EndCellY { get; set; }
        private int StartTextureCellX { get; set; }
        private int StartTextureCellY { get; set; }
        private int EndTextureCellX { get; set; }
        private int EndTextureCellY { get; set; }

        public bool IsEnabled { get; set; }

        public PaintTileEntity(GameResource graphic, int areaWidth, int areaHeight) :
            base("PaintTileEntity")
        {
            this.Processor = new GameResourceProcessor();
            this.Graphic = graphic;
            this.EntitySprite = new Sprite();
            this.EntitySprite.Visible = true;
            this.EntitySprite.Z = EngineConstants.AreaMaxLayers + 1;
            this.StartCellX = 0;
            this.StartCellY = 0;
            this.SourceRectangle = new Rectangle(0, 0, EngineConstants.TilePixelWidth, EngineConstants.TilePixelHeight);
            this.AreaWidth = areaWidth;
            this.AreaHeight = areaHeight;
            this.IsEnabled = false;

            LoadEntity();
        }

        protected override void CustomInitialize()
        {
        }

        protected override void CustomActivity()
        {
            this.EntitySprite.Visible = IsEnabled;

            if (InputManager.Mouse.IsInGameWindow() && IsEnabled)
            {
                StartCellX = Convert.ToInt32(InputManager.Mouse.WorldXAt(0) / EngineConstants.TilePixelWidth);
                StartCellY = Convert.ToInt32(InputManager.Mouse.WorldYAt(0) / EngineConstants.TilePixelHeight);

                this.X = StartCellX * (EngineConstants.TilePixelWidth);
                this.Y = StartCellY * (EngineConstants.TilePixelHeight);
                CheckBounds();
                PlaceTile();
                
            }
        }

        protected override void CustomDestroy()
        {
            SpriteManager.RemoveSprite(EntitySprite);
        }

        private void LoadEntity()
        {
            EntitySprite.PixelSize = 0.5f;
            EntitySprite.Alpha = 0.5f;
            EntitySprite.Texture = Processor.GetSubTexture(Graphic,
                StartCellX * EngineConstants.TilePixelWidth,
                StartCellY * EngineConstants.TilePixelHeight,
                EngineConstants.TilePixelWidth,
                EngineConstants.TilePixelHeight);

            EntitySprite.AttachTo(this, false);
            SpriteManager.AddSprite(EntitySprite);
        }

        public void SetTilesetCoordinates(int tileStartCellX, int tileStartCellY, int tileEndCellX, int tileEndCellY)
        {
            this.StartTextureCellX = tileStartCellX;
            this.StartTextureCellY = tileStartCellY;
            this.EndTextureCellX = tileEndCellX;
            this.EndTextureCellY = tileEndCellY;
            EntitySprite.Texture = Processor.GetSubTexture(Graphic,
                StartTextureCellX * EngineConstants.TilePixelWidth,
                StartTextureCellY * EngineConstants.TilePixelHeight, 
                (EndTextureCellX - StartTextureCellX + 1) * EngineConstants.TilePixelWidth,
                (EndTextureCellY - StartTextureCellY + 1) * EngineConstants.TilePixelHeight);

        }

        private void PlaceTile()
        {
            if (InputManager.Mouse.ButtonDown(Mouse.MouseButtons.LeftButton))
            {
                if (OnTilePainted != null)
                {
                    OnTilePainted(this, new TilePaintEventArgs(
                        StartCellX, 
                        StartCellY, 
                        EndCellX, 
                        EndCellY, 
                        StartTextureCellX,
                        StartTextureCellY,
                        EndTextureCellX,
                        EndTextureCellY,
                        Layer, 
                        EntitySprite.Texture));
                }
            }
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
