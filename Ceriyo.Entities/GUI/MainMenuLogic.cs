using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ceriyo.Data;
using Ceriyo.Data.Enumerations;
using Ceriyo.Data.EventArguments;
using Ceriyo.Entities.DrawableBatches;
using Ceriyo.Entities.GUI;
using Ceriyo.Library.SquidGUI;
using FlatRedBall;
using Squid;
using Ceriyo.Library.ScriptEngine;

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

        private UIWindow DirectConnectWindow { get; set; }
        private Button ConnectButton { get; set; }
        private Button CancelButton { get; set; }
        private TextBox IPAddressTextBox { get; set; }
        private TextBox PasswordTextBox { get; set; }

        #endregion

        #region Events

        public event EventHandler<DirectConnectEventArgs> OnDirectConnect;

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


            DirectConnectWindow = GetControl("DirectConnectWindow") as UIWindow;
            ConnectButton = GetControl("btnConnect") as Button;
            CancelButton = GetControl("btnCancel") as Button;
            IPAddressTextBox = GetControl("txtIPAddress") as TextBox;
            PasswordTextBox = GetControl("txtPassword") as TextBox;

            if (EngineConstants.IsDebugEnabled)
            {
                IPAddressTextBox.Text = "127.0.0.1";
            }
        }

        private void HookEvents()
        {
            DirectConnectButton.MouseClick += DirectConnectButton_MouseClick;
            ExitButton.MouseClick += ExitButton_MouseClick;

            ConnectButton.MouseClick += ConnectButton_MouseClick;
            CancelButton.MouseClick += CancelButton_MouseClick;
        }

        private void ConnectButton_MouseClick(Control sender, MouseEventArgs args)
        {
            if (OnDirectConnect != null)
            {
                DirectConnectEventArgs e = new DirectConnectEventArgs(IPAddressTextBox.Text, PasswordTextBox.Text);

                OnDirectConnect(this, e);
            }
        }

        private void CancelButton_MouseClick(Control sender, MouseEventArgs args)
        {
            DirectConnectWindow.Visible = false;
        }

        private void DirectConnectButton_MouseClick(Control sender, MouseEventArgs args)
        {
            DirectConnectWindow.Visible = true;
            DirectConnectWindow.BringToFront();
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
