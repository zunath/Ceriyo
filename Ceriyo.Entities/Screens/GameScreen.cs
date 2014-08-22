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
using Squid;
using Ceriyo.Entities.GUI;

namespace Ceriyo.Entities.Screens
{
    public class GameScreen : BaseScreen
    {
        MainMenuLogic _mainMenuGUI;
        DirectConnectLogic _directConnectGUI;

        public GameScreen()
            : base("GameScreen")
        {
            _mainMenuGUI = new MainMenuLogic();
            _directConnectGUI = new DirectConnectLogic();
        }

        protected override void CustomInitialize()
        {
            HookEvents();
        }

        protected override void CustomActivity(bool firstTimeCalled)
        {
        }

        protected override void CustomDestroy()
        {
            _mainMenuGUI.Destroy();
            _directConnectGUI.Destroy();
        }


        private void HookEvents()
        {
            _mainMenuGUI.DirectConnectButtonPressed += _directConnectGUI.Show;
        }

    }
}
