using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ceriyo.Entities.DrawableBatches;
using Squid;

namespace Ceriyo.Entities.GUI
{
    public class DirectConnectLogic : GUIDrawableBatch
    {
        #region Controls

        private UIWindow Window { get; set; }
        private Button ConnectButton { get; set; }
        private Button CancelButton { get; set; }
        private TextBox IPAddressTextBox { get; set; }
        private TextBox PasswordTextBox { get; set; }

        #endregion

        public DirectConnectLogic()
            : base("DirectConnect")
        {
            LoadControls();
            HookEvents();
        }

        private void LoadControls()
        {
            Window = GetControl("DirectConnectWindow") as UIWindow;
            ConnectButton = GetControl("btnConnect") as Button;
            CancelButton = GetControl("btnCancel") as Button;
            IPAddressTextBox = GetControl("txtIPAddress") as TextBox;
            PasswordTextBox = GetControl("txtPassword") as TextBox;
        }

        private void HookEvents()
        {
        }

        public void Show(object sender, EventArgs e)
        {
            Window.Visible = true;
        }
    }
}
