using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FlatRedBall;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Ceriyo.Toolset
{
    class ToolsetGame : FlatRedBallGameBase
    {
        public ToolsetGame(FlatRedBallControl frbControl)
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
