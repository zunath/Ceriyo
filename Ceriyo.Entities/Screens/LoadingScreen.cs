using Ceriyo.Entities.Entities;
using FlatRedBall;
using Microsoft.Xna.Framework;

namespace Ceriyo.Entities.Screens
{
    public class LoadingScreen : BaseScreen
    {
        private readonly LoadingEntity _loadingEntity;

        public LoadingScreen()
            : base("LoadingScreen")
        {
            _loadingEntity = new LoadingEntity();
            SpriteManager.Camera.BackgroundColor = Color.Black;
        }

        protected override void CustomInitialize()
        {
        }

        protected override void CustomActivity(bool firstTimeCalled)
        {
            if (AsyncLoadingState == FlatRedBall.Screens.AsyncLoadingState.NotStarted)
            {
                StartAsyncLoad(typeof(GameScreen));
            }
            else if (AsyncLoadingState == FlatRedBall.Screens.AsyncLoadingState.Done)
            {
                IsActivityFinished = true;
            }
        }

        protected override void CustomDestroy()
        {
            _loadingEntity.Destroy();
        }


    }
}
