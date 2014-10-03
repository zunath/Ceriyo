using Ceriyo.Entities.DrawableBatches;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
