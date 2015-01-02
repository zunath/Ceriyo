using System;
using System.ComponentModel;
using Ceriyo.Data.Engine;
using Ceriyo.Data.EventArguments;
using Ceriyo.Data.GameObjects;
using Ceriyo.Entities.DrawableBatches;
using FlatRedBall;

namespace Ceriyo.Entities.Screens
{
    public class AreaEditorScreen : BaseScreen
    {
        private Area LoadedArea { get; set; }
        private EditableMapDrawableBatch AreaBatch { get; set; }
        private event EventHandler<AreaPropertiesChangedEventArgs> OnAreaPropertiesSaved;
        private event EventHandler<ObjectPainterEventArgs> OnObjectPainterModeChangeReceived;

        public AreaEditorScreen()
            : base("AreaEditorScreen")
        {
        }

        protected override void CustomInitialize()
        {
        }

        protected override void CustomActivity(bool firstTimeCalled)
        {
        }

        protected override void CustomDestroy()
        {
            OnObjectPainterModeChangeReceived -= AreaBatch.ChangeObjectSelectionMode;
            AreaBatch.Destroy();
        }

        public void CloseArea(object sender, EventArgs e)
        {
            if (AreaBatch != null)
            {
                SpriteManager.RemoveDrawableBatch(AreaBatch);
                OnAreaPropertiesSaved -= AreaBatch.AreaPropertiesSaved;
                AreaBatch.Destroy();
            }

            LoadedArea = null;
        }

        public void LoadArea(object sender, GameObjectEventArgs e)
        {
            CloseArea(sender, e);

            LoadedArea = WorkingDataManager.GetGameObject<Area>(ModulePaths.AreasDirectory, e.GameObject.Resref);
            AreaBatch = new EditableMapDrawableBatch(LoadedArea);
            SpriteManager.AddDrawableBatch(AreaBatch);

            OnAreaPropertiesSaved += AreaBatch.AreaPropertiesSaved;
            OnObjectPainterModeChangeReceived += AreaBatch.ChangeObjectSelectionMode;
        }

        public void SaveArea(object sender, EventArgs e)
        {
            LoadedArea.MapTiles = new BindingList<MapTile>(AreaBatch.GetMapTiles());

            WorkingDataManager.SaveGameObjectFile(LoadedArea);
        }

        public void AreaPropertiesSaved(object sender, AreaPropertiesChangedEventArgs e)
        {
            if (!e.IsUpdate)
            {
                if (LoadedArea != null)
                {
                    if (e.ModifiedArea.Resref != LoadedArea.Resref)
                    {
                        LoadArea(sender, new GameObjectEventArgs(e.ModifiedArea));
                    }
                }
                else
                {
                    LoadArea(sender, new GameObjectEventArgs(e.ModifiedArea));
                }
            }

            if (OnAreaPropertiesSaved != null)
            {
                OnAreaPropertiesSaved(sender, e);
            }
        }

        public void PaintObjectModeChanged(object sender, ObjectPainterEventArgs e)
        {
            if (OnObjectPainterModeChangeReceived != null)
            {
                OnObjectPainterModeChangeReceived(sender, e);
            }
        }
    }
}
