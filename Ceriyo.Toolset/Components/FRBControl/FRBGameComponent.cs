using System;
using FlatRedBall;
using FlatRedBall.Screens;
using Microsoft.Xna.Framework;

namespace Ceriyo.Toolset.Components.FRBControl
{
    class FRBGameComponent : FlatRedBallGameBase
    {
        public FRBGameComponent(FlatRedBallControl frbControl, Type screenType )
            : base(frbControl)
        {
            ScreenManager.Start(screenType);
        }

        protected override void Initialize()
        {
            base.Initialize();
            SpriteManager.Camera.UsePixelCoordinates();
            SpriteManager.Camera.BackgroundColor = Color.LightGray;
            IsRenderingPaused = false;
        }

        protected override void Update(GameTime gameTime)
        {
            FlatRedBallServices.Update(gameTime);
            ScreenManager.Activity();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            if (IsRenderingPaused)
            {
                return;
            }

            FlatRedBallServices.Draw();
            base.Draw(gameTime);
        }
    }
}
