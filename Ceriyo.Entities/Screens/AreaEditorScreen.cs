using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ceriyo.Data.EventArguments;
using Ceriyo.Data.GameObjects;
using FlatRedBall;
using FlatRedBall.Input;
using FlatRedBall.Screens;

namespace Ceriyo.Entities.Screens
{
    public class AreaEditorScreen : BaseScreen
    {
        private Area LoadedArea { get; set; }
        private MapDrawableBatch AreaBatch;
        
        public AreaEditorScreen()
            : base("AreaEditorScreen")
        {
        }

        protected override void CustomInitialize()
        {
        }

        protected override void CustomActivity(bool firstTimeCalled)
        {
            if(InputManager.Mouse.ButtonDown(Mouse.MouseButtons.RightButton))
            {
                SpriteManager.Camera.X -= InputManager.Mouse.XChange;
                SpriteManager.Camera.Y += InputManager.Mouse.YChange;
            }
        }

        protected override void CustomDestroy()
        {
            
        }

        public void CloseArea(object sender, EventArgs e)
        {
            if (AreaBatch != null)
            {
                AreaBatch.Destroy();
            }

            LoadedArea = null;
        }

        public void LoadArea(object sender, GameObjectEventArgs e)
        {
            if (AreaBatch != null)
            {
                AreaBatch.Destroy();
            }

            LoadedArea = e.GameObject as Area;
            AreaBatch = new MapDrawableBatch(LoadedArea);
        }

        public void OnModulePropertiesUpdate(object sender, GameObjectEventArgs e)
        {
            if (LoadedArea != null)
            {
                if (e.GameObject.Resref == LoadedArea.Resref)
                {
                    LoadArea(sender, e);
                }
            }
        }
    }
}
