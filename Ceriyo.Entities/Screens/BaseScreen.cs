using Ceriyo.Data;
using FlatRedBall.Input;
using FlatRedBall.Screens;
using Microsoft.Xna.Framework.Input;

namespace Ceriyo.Entities.Screens
{
    public abstract class BaseScreen : Screen
    {
        protected BaseScreen(string contentManagerName)
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
        
        protected abstract void CustomActivity(bool firstTimeCalled);
        public override void Activity(bool firstTimeCalled)
        {
            base.Activity(firstTimeCalled);
            CustomActivity(firstTimeCalled);


            if (EngineConstants.IsDebugEnabled)
            {
                if (InputManager.Keyboard.KeyPushed(Keys.F5))
                {
                    MoveToScreen(GetType());
                }

                //MoveToScreen(GetType()); // For layout designing
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
