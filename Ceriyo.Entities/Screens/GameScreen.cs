using FlatRedBall;
using FlatRedBall.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ceriyo.Data.GameObjects;
using Ceriyo.Entities.DrawableBatches;
using Ceriyo.Library.SquidGUI;
using Ceriyo.Data.ResourceObjects;
using FlatRedBall.IO;

namespace Ceriyo.Entities.Screens
{
    public class GameScreen : BaseScreen
    {
        GUIDrawableBatch _gui;

        public GameScreen()
            : base("GameScreen")
        {
            SquidLayoutManager manager = new SquidLayoutManager();

            _gui = new GUIDrawableBatch(manager.LayoutToDesktop("MainMenuLayout"));
        }

        protected override void CustomInitialize()
        {
        }

        protected override void CustomActivity(bool firstTimeCalled)
        {
        }

        protected override void CustomDestroy()
        {
            _gui.Destroy();
        }
    }
}
