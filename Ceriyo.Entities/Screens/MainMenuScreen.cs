using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ceriyo.Entities.Entities.GUI;
using FlatRedBall;
using FlatRedBall.Graphics;

namespace Ceriyo.Entities.Screens
{
    public class MainMenuScreen : BaseScreen
    {
        private ButtonEntity ConnectButton { get; set; }
        private ButtonEntity ExitButton { get; set; }
        private Layer GUILayer { get; set; }

        public MainMenuScreen()
            : base("MainMenuScreen")
        {
            GUILayer = new Layer();
            ConnectButton = new ButtonEntity("Connect");
            ExitButton = new ButtonEntity("Exit");
        }

        protected override void CustomInitialize()
        {
            SpriteManager.Camera.AddLayer(GUILayer);
            ConnectButton.InitializeEntity(true);
            ExitButton.InitializeEntity(true);

            ConnectButton.Y += 50;
        }

        protected override void CustomActivity(bool firstTimeCalled)
        {
            ConnectButton.Activity();
            ExitButton.Activity();
        }

        protected override void CustomDestroy()
        {
            SpriteManager.Camera.RemoveLayer(GUILayer);
            ConnectButton.Destroy();
            ExitButton.Destroy();
        }
    }
}
