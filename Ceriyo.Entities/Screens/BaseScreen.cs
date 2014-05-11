using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FlatRedBall.Screens;

namespace Ceriyo.Entities.Screens
{
    public abstract class BaseScreen : Screen
    {
        public BaseScreen(string contentManagerName)
            : base(contentManagerName)
        {
        }

        protected abstract void CustomInitialize();
        public override void Initialize(bool addToManagers)
        {
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
        }

        protected abstract void CustomDestroy();
        public override void Destroy()
        {
            base.Destroy();
            CustomDestroy();
        }

    }
}
