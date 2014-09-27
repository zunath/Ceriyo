using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ceriyo.Entities.GUI;

namespace Ceriyo.Entities.Screens
{
    public class CharacterCreationScreen : BaseScreen
    {
        private CharacterCreationMenuLogic GUI { get; set; }

        public CharacterCreationScreen()
            : base("CharacterCreationScreen")
        {

        }

        protected override void CustomInitialize()
        {
            GUI = new CharacterCreationMenuLogic();
        }

        protected override void CustomActivity(bool firstTimeCalled)
        {
            
        }

        protected override void CustomDestroy()
        {
            GUI.Destroy();
        }
    }
}
