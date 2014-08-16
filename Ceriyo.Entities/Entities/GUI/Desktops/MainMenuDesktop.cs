using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ceriyo.Entities.Entities.GUI.Components;
using Ceriyo.Entities.GUI;
using Squid;

namespace Ceriyo.Entities.Entities.GUI.Desktops
{
    public class MainMenuDesktop : BaseDesktop
    {
        public MainMenuDesktop()
        {
            LoginMenuUIComponent loginMenu = new LoginMenuUIComponent(this);
            

        }

    }
}
