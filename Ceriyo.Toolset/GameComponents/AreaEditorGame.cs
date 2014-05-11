using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ceriyo.Data.GameObjects;
using Ceriyo.Entities;
using Ceriyo.Entities.Screens;
using FlatRedBall;
using FlatRedBall.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Ceriyo.Toolset.GameComponents
{
    class AreaEditorGame : FlatRedBallGameBase
    {
        private MapDrawableBatch AreaBatch { get; set; }

        public AreaEditorGame(FlatRedBallControl frbControl)
            : base(frbControl)
        {

        }

        protected override void Initialize()
        {
            base.Initialize();

            SpriteManager.Camera.UsePixelCoordinates();
            SpriteManager.Camera.BackgroundColor = Color.LightGray;

            ScreenManager.Start(typeof(AreaEditorScreen));
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
