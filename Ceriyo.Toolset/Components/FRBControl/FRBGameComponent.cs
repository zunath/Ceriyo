using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ceriyo.Entities.Screens;
using Ceriyo.Toolset.FRBControl;
using FlatRedBall;
using FlatRedBall.Screens;
using Microsoft.Xna.Framework;

namespace Ceriyo.Toolset.FRBControl
{
    class FRBGameComponent : FlatRedBallGameBase
    {

        public FRBGameComponent(FlatRedBallControl frbControl, Type screenType )
            : base(frbControl)
        {
        }

        protected override void Initialize()
        {
            base.Initialize();
            SpriteManager.Camera.UsePixelCoordinates();
            SpriteManager.Camera.BackgroundColor = Color.LightGray;
        }

        protected override void Update(GameTime gameTime)
        {
            FlatRedBallServices.Update(gameTime);
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
