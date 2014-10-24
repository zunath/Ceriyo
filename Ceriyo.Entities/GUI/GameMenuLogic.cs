using Ceriyo.Entities.DrawableBatches;

namespace Ceriyo.Entities.GUI
{
    public class GameMenuLogic : GUIDrawableBatch
    {
        #region Controls

        #endregion

        public GameMenuLogic()
            : base("GameMenu")
        {
            LoadControls();
            HookEvents();
        }

        private void LoadControls()
        {
        }

        private void HookEvents()
        {
        }
    }
}
