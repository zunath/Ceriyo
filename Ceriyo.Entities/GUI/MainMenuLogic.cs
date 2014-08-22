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

namespace Ceriyo.Entities.GUI
{
    public class MainMenuLogic : GUIDrawableBatch
    {
        #region Controls

        private UIWindow Window { get; set; }
        private Button LoginButton { get; set; }
        private Button ServerListButton { get; set; }
        private Button DirectConnectButton { get; set; }
        private Button SettingsButton { get; set; }
        private Button ExitButton { get; set; }

        #endregion

        #region Events

        public event EventHandler<EventArgs> DirectConnectButtonPressed;

        #endregion

        public MainMenuLogic()
            : base("MainMenu")
        {
            LoadControls();
            HookEvents();
        }

        private void LoadControls()
        {
            Window = GetControl("MainMenuWindow") as UIWindow;
            LoginButton = GetControl("btnLogin") as Button;
            ServerListButton = GetControl("btnServerList") as Button;
            DirectConnectButton = GetControl("btnDirectConnect") as Button;
            SettingsButton = GetControl("btnSettings") as Button;
            ExitButton = GetControl("btnExit") as Button;
        }

        private void HookEvents()
        {
            DirectConnectButton.MouseClick += DirectConnectButton_MouseClick;
            ExitButton.MouseClick += ExitButton_MouseClick;
        }

        private void DirectConnectButton_MouseClick(Control sender, MouseEventArgs args)
        {
            if (DirectConnectButtonPressed != null)
            {
                DirectConnectButtonPressed(this, new EventArgs());
            }
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
