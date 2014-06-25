using Ceriyo.Entities.Entities.GUI;
using FlatRedBall;
using FlatRedBall.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ceriyo.Entities.Screens
{
    public class GameScreen : BaseScreen
    {
        private ButtonEntity button { get; set; }
        private Layer GUILayer { get; set; }

        public GameScreen()
            : base("GameScreen")
        {
            GUILayer = new Layer();
        }

        protected override void CustomInitialize()
        {
            SpriteManager.Camera.AddLayer(GUILayer);
            button = new ButtonEntity("Test text");
        }

        protected override void CustomActivity(bool firstTimeCalled)
        {
            button.Activity();
        }

        protected override void CustomDestroy()
        {
            SpriteManager.Camera.RemoveLayer(GUILayer);
            button.Destroy();
        }
    }
}
