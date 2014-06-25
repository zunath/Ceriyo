using Ceriyo.Entities.Entities.GUI;
using FlatRedBall;
using FlatRedBall.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ceriyo.Data.GameObjects;

namespace Ceriyo.Entities.Screens
{
    public class GameScreen : BaseScreen
    {
        private WindowEntity window;

        public GameScreen()
            : base("GameScreen")
        {
            window = new WindowEntity(400, 200, "title");
        }

        protected override void CustomInitialize()
        {
            window.InitializeEntity(true);

        }

        protected override void CustomActivity(bool firstTimeCalled)
        {
            window.Activity();
        }

        protected override void CustomDestroy()
        {
            window.Destroy();
        }
    }
}
