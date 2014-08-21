using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ceriyo.Data.Enumerations;
using Ceriyo.Entities.DrawableBatches;
using Ceriyo.Entities.GUI;
using Ceriyo.Library.SquidGUI;
using FlatRedBall;
using Squid;

namespace Ceriyo.Entities.Entities.GUI
{
    public class MainMenuGUIEntity : GUIDrawableBatch
    {
        #region Controls

        UIWindow Window { get; set; }
        Button LoginButton { get; set; }
        Button FindServerButton { get; set; }
        Button SettingsButton { get; set; }
        Button ExitButton { get; set; }

        #endregion

        public MainMenuGUIEntity()
            : base("MainMenuLayout")
        {
            LoadControls();
            HookEvents();
        }

        private void LoadControls()
        {
            Window = GetControl("MainMenuWindow") as UIWindow;
            LoginButton = GetControl("btnLogin") as Button;
            FindServerButton = GetControl("btnFindServer") as Button;
            SettingsButton = GetControl("btnSettings") as Button;
            ExitButton = GetControl("btnExit") as Button;
        }

        private void HookEvents()
        {
            ExitButton.MouseClick += ExitButton_MouseClick;
        }

        private void ExitButton_MouseClick(Control sender, MouseEventArgs args)
        {
            MessageBox box = MessageBox.Show(new Point(200, 150), "Really Exit?", "Are you sure you want to exit?", MessageBoxButtonTypeEnum.YesNo, _desktop);
            box.OnYesClicked += ExitConfirmClicked;   

        }

        private void ExitConfirmClicked(object sender, EventArgs e)
        {
            FlatRedBallServices.Game.Exit();
        }
    }
}
