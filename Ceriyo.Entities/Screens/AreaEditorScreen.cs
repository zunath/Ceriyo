using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Ceriyo.Data;
using Ceriyo.Data.Engine;
using Ceriyo.Data.Enumerations;
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
        private MapDrawableBatch AreaBatch { get; set; }
        private PaintTileEntity PaintTile { get; set; }
        private PaintCreatureEntity PaintCreature { get; set; }
        private WorkingDataManager WorkingManager { get; set; }
        private event EventHandler<ObjectPainterEventArgs> OnPaintObjectChanged;

        public AreaEditorScreen()
            : base("AreaEditorScreen")
        {
        }

        protected override void CustomInitialize()
        {
            WorkingManager = new WorkingDataManager();
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
            PaintTile.OnTilePainted -= AreaBatch.PaintTile;
            AreaBatch.Destroy();
            PaintTile.Destroy();
        }

        public void CloseArea(object sender, EventArgs e)
        {
            if (AreaBatch != null)
            {
                AreaBatch.Destroy();
            }

            if (AreaBatch != null)
            {
                PaintTile.OnTilePainted -= AreaBatch.PaintTile;
            }

            if (PaintTile != null)
            {
                PaintTile.Destroy();
            }

            LoadedArea = null;
        }

        public void LoadArea(object sender, GameObjectEventArgs e)
        {
            if (AreaBatch != null)
            {
                AreaBatch.Destroy();
            }

            LoadedArea = WorkingManager.GetGameObject<Area>(ModulePaths.AreasDirectory, e.GameObject.Resref);
            AreaBatch = new MapDrawableBatch(LoadedArea);

            if (PaintTile != null)
            {
                PaintTile.Destroy();
            }

            PaintTile = new PaintTileEntity(LoadedArea.AreaTileset.Graphic, LoadedArea.MapWidth, LoadedArea.MapHeight);

            PaintTile.OnTilePainted += AreaBatch.PaintTile;
        }

        public void SaveArea(object sender, EventArgs e)
        {
            LoadedArea.MapTiles = new BindingList<MapTile>(AreaBatch.GetMapTiles());

            WorkingManager.SaveGameObjectFile(LoadedArea);
        }

        public void OnAreaPropertiesUpdate(object sender, AreaPropertiesChangedEventArgs e)
        {
            if (!e.IsUpdate)
            {
                if (LoadedArea != null)
                {
                    if (e.ModifiedArea.Resref != LoadedArea.Resref)
                    {
                        PaintTile.Destroy();
                        LoadArea(sender, new GameObjectEventArgs(e.ModifiedArea));
                    }
                }
                else
                {
                    LoadArea(sender, new GameObjectEventArgs(e.ModifiedArea));
                }
            }
        }

        public void ChangePaintMode(object sender, ObjectPainterEventArgs e)
        {
            if (e.GameObject == null)
            {
                PaintTile.SetTilesetCoordinates(e.TileCellXStart, e.TileCellYStart, e.TileCellXEnd, e.TileCellYEnd);
                PaintTile.IsEnabled = true;
            }
            else
            {
                PaintTile.IsEnabled = false;
            }
        }
    }
}
