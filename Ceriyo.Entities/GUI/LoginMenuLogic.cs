using Ceriyo.Entities.DrawableBatches;
using Squid;

namespace Ceriyo.Entities.GUI
{
    public class LoginMenuLogic : GUIDrawableBatch
    {
        #region Controls

        private UIWindow Window { get; set; }
        private Label UsernameLabel { get; set; }
        private TextBox UsernameTextBox { get; set; }
        private Label PasswordLabel { get; set; }
        private TextBox PasswordTextBox { get; set; }
        private Button LoginButton { get; set; }
        private CheckBox RememberMeCheckBox { get; set; }

        #endregion

        public LoginMenuLogic()
            : base("LoginMenu")
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
