using FlatRedBall;
using FlatRedBall.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ceriyo.Data.GameObjects;
using Ceriyo.Entities.DrawableBatches;
using Ceriyo.Library.SquidGUI;
using Ceriyo.Data.ResourceObjects;
using FlatRedBall.IO;

namespace Ceriyo.Entities.Screens
{
    public class GameScreen : BaseScreen
    {
        GUIDrawableBatch _gui;

        public GameScreen()
            : base("GameScreen")
        {
            // DEBUGGING

            //UILayout layout = new UILayout();
            //UIComponent window = new UIComponent
            //{
            //    Anchor = Squid.AnchorStyles.None,
            //    ComponentType = "Window",
            //    CursorType = Squid.Cursors.Default,
            //    PositionX = 40,
            //    PositionY = 40,
            //    SizeX = 440,
            //    SizeY = 340,
            //    Title = "Log In",
            //    Resizeable = false
            //};

            //UIComponent label = new UIComponent
            //{
            //    Anchor = Squid.AnchorStyles.None,
            //    ComponentType = "Label",
            //    CursorType = Squid.Cursors.Default,
            //    PositionX = 60,
            //    PositionY = 100,
            //    Resizeable = false,
            //    SizeX = 122,
            //    SizeY = 35,
            //    Text = "Username:",
            //    Title = string.Empty
            //};

            //window.Children.Add(label);

            //layout.Components.Add(window);
            //FileManager.XmlSerialize(layout, "C:\\Users\\Public\\Desktop\\test.xml");

            // END DEBUGGING

            SquidLayoutManager manager = new SquidLayoutManager();

            _gui = new GUIDrawableBatch(manager.LayoutToDesktop("LoginMenuLayout"));
        }

        protected override void CustomInitialize()
        {
        }

        protected override void CustomActivity(bool firstTimeCalled)
        {
        }

        protected override void CustomDestroy()
        {
            _gui.Destroy();
        }
    }
}
