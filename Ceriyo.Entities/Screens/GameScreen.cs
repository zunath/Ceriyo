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
        private CheckBoxEntity checkbox { get; set; }
        private Layer GUILayer { get; set; }

        public GameScreen()
            : base("GameScreen")
        {
            GUILayer = new Layer();
            button = new ButtonEntity("Login");
            checkbox = new CheckBoxEntity("short text");
        }

        protected override void CustomInitialize()
        {
            SpriteManager.Camera.AddLayer(GUILayer);
            button.InitializeEntity(true);
            checkbox.InitializeEntity(true);
            checkbox.Y = checkbox.Y + 100;

        }

        protected override void CustomActivity(bool firstTimeCalled)
        {
            button.Activity();
            checkbox.Activity();
        }

        protected override void CustomDestroy()
        {
            SpriteManager.Camera.RemoveLayer(GUILayer);
            button.Destroy();
            checkbox.Destroy();
        }
    }
}
