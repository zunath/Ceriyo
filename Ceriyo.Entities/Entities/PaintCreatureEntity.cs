using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ceriyo.Data;
using Ceriyo.Data.EventArguments;
using Ceriyo.Library.Processing;
using FlatRedBall;
using FlatRedBall.Input;

namespace Ceriyo.Entities.Entities
{
    public class PaintCreatureEntity : GraphicEntity
    {
        public event EventHandler<ObjectPainterEventArgs> OnCreaturePainted;
        public bool IsEnabled { get; set; }
        private int AreaWidth { get; set; }
        private int AreaHeight { get; set; }

        public PaintCreatureEntity(int areaWidth, int areaHeight)
            : base("PaintCreatureEntity")
        {
            this.IsEnabled = false;
            this.AreaWidth = areaWidth;
            this.AreaHeight = areaHeight;
        }

        protected override void CustomInitialize()
        {
            this.EntitySprite.Z = EngineConstants.AreaMaxLayers + 1;
            this.EntitySprite.PixelSize = 0.5f;
            this.EntitySprite.Alpha = 0.5f;
        }

        protected override void CustomActivity()
        {
            this.EntitySprite.Visible = IsEnabled;

            if (InputManager.Mouse.IsInGameWindow() && IsEnabled)
            {
                this.X = InputManager.Mouse.WorldXAt(0);
                this.Y = InputManager.Mouse.WorldYAt(0);
            }
        }

        protected override void CustomDestroy()
        {
            SpriteManager.RemoveSprite(EntitySprite);
        }

        private void LoadEntity()
        {
            //EntitySprite.Texture = Processor
        }
    }
}
