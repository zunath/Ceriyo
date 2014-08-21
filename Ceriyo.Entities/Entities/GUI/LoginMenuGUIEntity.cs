using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ceriyo.Entities.DrawableBatches;
using Ceriyo.Entities.GUI;
using Ceriyo.Library.SquidGUI;
using Squid;

namespace Ceriyo.Entities.Entities.GUI
{
    public class LoginMenuGUIEntity : GUIDrawableBatch
    {
        #region Controls

        UIWindow Window { get; set; }
        Label UsernameLabel { get; set; }
        TextBox UsernameTextBox { get; set; }
        Label PasswordLabel { get; set; }
        TextBox PasswordTextBox { get; set; }
        Button LoginButton { get; set; }
        CheckBox RememberMeCheckBox { get; set; }

        #endregion

        public LoginMenuGUIEntity()
            : base("MainMenuLayout")
        {
            LoadControls();
            HookEvents();
        }

        private void LoadControls()
        {
            Window = GetControl("LoginWindow") as UIWindow;
            UsernameLabel = GetControl("lblUserName") as Label;
            UsernameTextBox = GetControl("txtUsername") as TextBox;
            PasswordLabel = GetControl("lblPassword") as Label;
            PasswordTextBox = GetControl("txtPassword") as TextBox;
            LoginButton = GetControl("btnLogin") as Button;
            RememberMeCheckBox = GetControl("chkRememberMe") as CheckBox;
        }

        private void HookEvents()
        {

        }
    }
}
