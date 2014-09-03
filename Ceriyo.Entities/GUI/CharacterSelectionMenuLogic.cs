using Ceriyo.Entities.DrawableBatches;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ceriyo.Entities.GUI
{
    public class CharacterSelectionMenuLogic : GUIDrawableBatch
    {
        #region Controls

        #endregion

        public CharacterSelectionMenuLogic()
            : base("CharacterSelectionMenu")
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
