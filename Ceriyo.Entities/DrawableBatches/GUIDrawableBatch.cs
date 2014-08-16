using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FlatRedBall;
using FlatRedBall.Graphics;
using Ceriyo.Library.SquidGUI;
using Squid;
using SampleControls;

namespace Ceriyo.Entities.DrawableBatches
{
    public class GUIDrawableBatch : PositionedObject, IDrawableBatch
    {
        private SquidInputManager _inputManager;
        private Desktop _desktop;

        public GUIDrawableBatch()
        {
            _desktop = new SampleDesktop { Name = "desk" };
            _desktop.ShowCursor = true;

            GuiHost.Renderer = new SquidRendererXNA();
            _inputManager = new SquidInputManager();
            FlatRedBallServices.Game.Components.Add(_inputManager);

            SpriteManager.AddDrawableBatch(this);
        }

        public void Destroy()
        {
            FlatRedBallServices.Game.Components.Remove(_inputManager);

            GuiHost.Renderer = null;

            SpriteManager.RemoveDrawableBatch(this);
        }

        public void Draw(Camera camera)
        {
            GuiHost.TimeElapsed = (float)TimeManager.LastUpdateGameTime.ElapsedGameTime.TotalMilliseconds;

            _desktop.Size = new Squid.Point(FlatRedBallServices.Game.GraphicsDevice.Viewport.Width, 
                FlatRedBallServices.Game.GraphicsDevice.Viewport.Height);
            _desktop.Update();
            _desktop.Draw();

        }

        public void Update()
        {
        }

        public bool UpdateEveryFrame
        {
            get { return true; }
        }
    }
}
