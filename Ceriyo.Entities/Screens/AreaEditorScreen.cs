using System;
using Ceriyo.Data;
using Ceriyo.Data.Engine;
using Ceriyo.Data.EventArguments;
using Ceriyo.Data.GameObjects;
using Ceriyo.Entities.DrawableBatches;
using FlatRedBall;
using FlatRedBall.Input;

namespace Ceriyo.Entities.Screens
{
    public class AreaEditorScreen : BaseScreen
    {
        private Area LoadedArea { get; set; }
        private EditableMapDrawableBatch AreaBatch { get; set; }
        private event EventHandler<AreaPropertiesChangedEventArgs> OnAreaPropertiesSaved;

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
            AreaBatch.Destroy();
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
                OnAreaPropertiesSaved -= AreaBatch.AreaPropertiesSaved;
            }

            LoadedArea = WorkingDataManager.GetGameObject<Area>(ModulePaths.AreasDirectory, e.GameObject.Resref);
            AreaBatch = new EditableMapDrawableBatch(LoadedArea);

            OnAreaPropertiesSaved += AreaBatch.AreaPropertiesSaved;
        }

        public void SaveArea(object sender, EventArgs e)
        {
            //LoadedArea.MapTiles = AreaBatch.MapTiles;

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

    }
}
