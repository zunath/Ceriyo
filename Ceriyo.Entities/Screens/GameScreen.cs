﻿using Ceriyo.Entities.Entities.GUI;
using FlatRedBall;
using FlatRedBall.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ceriyo.Data.GameObjects;
using Ceriyo.Entities.DrawableBatches;

namespace Ceriyo.Entities.Screens
{
    public class GameScreen : BaseScreen
    {
        GUIDrawableBatch _gui;

        public GameScreen()
            : base("GameScreen")
        {
            _gui = new GUIDrawableBatch(new SampleControls.SampleDesktop { Name = "desk" });
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
