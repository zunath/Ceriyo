using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ceriyo.Data.GameObjects;
using Ceriyo.Library.CustomDrawableBatches;
using FlatRedBall;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Ceriyo.Toolset
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

            AreaBatch = new MapDrawableBatch(new Area("test", "test", "test", 10, 10, 4));
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
