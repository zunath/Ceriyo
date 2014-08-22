using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ceriyo.Data;
using FlatRedBall.Input;
using FlatRedBall.Screens;
using Microsoft.Xna.Framework.Input;

namespace Ceriyo.Entities.Screens
{
    public abstract class BaseScreen : Screen
    {
        private bool AutoRefreshScreen { get; set; }

        public BaseScreen(string contentManagerName)
            : base(contentManagerName)
        {
        }

        protected abstract void CustomInitialize();
        public override void Initialize(bool addToManagers)
        {
            AutoRefreshScreen = false;
            base.Initialize(addToManagers);

            if (addToManagers)
            {
                AddToManagers();
            }

            CustomInitialize();
        }

        public override void AddToManagers()
        {
            base.AddToManagers();
        }

        protected abstract void CustomActivity(bool firstTimeCalled);
        public override void Activity(bool firstTimeCalled)
        {
            base.Activity(firstTimeCalled);
            CustomActivity(firstTimeCalled);


            if (EngineConstants.IsDebugEnabled)
            {
                if (InputManager.Keyboard.KeyPushed(Keys.F5))
                {
                    this.MoveToScreen(this.GetType());
                }

                //this.MoveToScreen(this.GetType()); // For layout designing
            }
        }

        protected abstract void CustomDestroy();
        public override void Destroy()
        {
            base.Destroy();
            CustomDestroy();
        }

    }
}
