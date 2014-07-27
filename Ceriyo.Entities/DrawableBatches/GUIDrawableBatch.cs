using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FlatRedBall;
using FlatRedBall.Graphics;
using Ceriyo.Library.SquidGUI;
using Squid;

namespace Ceriyo.Entities.DrawableBatches
{
    public class GUIDrawableBatch : PositionedObject, IDrawableBatch
    {
        private SquidInputManager _inputManager;
        private SampleScene _scene;

        public GUIDrawableBatch()
        {
            GuiHost.Renderer = new SquidRendererXNA();
            _inputManager = new SquidInputManager();
            FlatRedBallServices.Game.Components.Add(_inputManager);

            _scene = new SampleScene();
            FlatRedBallServices.Game.Components.Add(_scene);

            SpriteManager.AddDrawableBatch(this);
        }

        public void Destroy()
        {
            FlatRedBallServices.Game.Components.Remove(_scene);
            FlatRedBallServices.Game.Components.Remove(_inputManager);

            GuiHost.Renderer = null;

            SpriteManager.RemoveDrawableBatch(this);
        }

        public void Draw(Camera camera)
        {
            GuiHost.TimeElapsed = (float)TimeManager.LastUpdateGameTime.ElapsedGameTime.TotalMilliseconds;

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
