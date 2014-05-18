using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ceriyo.Data.Engine;
using Ceriyo.Data.EventArguments;
using Ceriyo.Data.GameObjects;
using Ceriyo.Entities.Entities;
using FlatRedBall;
using FlatRedBall.Input;
using FlatRedBall.Screens;

namespace Ceriyo.Entities.Screens
{
    public class AreaEditorScreen : BaseScreen
    {
        private Area LoadedArea { get; set; }
        private MapDrawableBatch AreaBatch { get; set; }
        private PaintTileEntity PaintTile { get; set; }

        private event EventHandler<ObjectPainterEventArgs> OnPaintObjectChanged;

        public AreaEditorScreen()
            : base("AreaEditorScreen")
        {
        }

        protected override void CustomInitialize()
        {
            
        }

        protected override void CustomActivity(bool firstTimeCalled)
        {
            if (PaintTile != null)
            {
                PaintTile.Activity();
            }

            if(InputManager.Mouse.ButtonDown(Mouse.MouseButtons.RightButton))
            {
                SpriteManager.Camera.X -= InputManager.Mouse.XChange;
                SpriteManager.Camera.Y += InputManager.Mouse.YChange;
            }
        }

        protected override void CustomDestroy()
        {
            AreaBatch.Destroy();
            PaintTile.Destroy();
        }

        public void CloseArea(object sender, EventArgs e)
        {
            if (AreaBatch != null)
            {
                AreaBatch.Destroy();
            }

            LoadedArea = null;
            PaintTile = null;
        }

        public void LoadArea(object sender, GameObjectEventArgs e)
        {
            if (AreaBatch != null)
            {
                AreaBatch.Destroy();
            }

            LoadedArea = e.GameObject as Area;
            AreaBatch = new MapDrawableBatch(LoadedArea);
            PaintTile = new PaintTileEntity(LoadedArea.AreaTileset.Graphic);
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

        public void ChangePaintMode(object sender, ObjectPainterEventArgs e)
        {
            if (e.GameObject == null)
            {
                PaintTile.SetTilesetCoordinates(e.TileCellX, e.TileCellY);
            }
        }
    }
}
