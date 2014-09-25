
using FlatRedBall;
using FlatRedBall.Screens;
using Microsoft.Xna.Framework;
using Ceriyo.Entities.Screens;
using Ceriyo.Data.Engine;
using FlatRedBall.Graphics;
using Ceriyo.Library.Global;
using Ceriyo.Data.Enumerations;
using Ceriyo.Library.Network;


namespace Ceriyo
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        private GraphicsDeviceManager _graphics;
        private EngineDataManager EngineManager { get; set; }

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            EngineManager = new EngineDataManager();

#if WINDOWS_PHONE || ANDROID || IOS

			// Frame rate is 30 fps by default for Windows Phone,
            // so let's keep that for other phones too
            TargetElapsedTime = TimeSpan.FromTicks(333333);
            graphics.IsFullScreen = true;
#else
            _graphics.PreferredBackBufferHeight = 600;
#endif
        }

        protected override void Initialize()
        {
            FlatRedBallServices.InitializeFlatRedBall(this, _graphics);
			CameraSetup.SetupCamera(SpriteManager.Camera, _graphics);
            EngineManager.InitializeEngine();
			CameraSetup.SetupCamera(SpriteManager.Camera, _graphics);
			GlobalContent.Initialize();
            FlatRedBallServices.IsWindowsCursorVisible = true;
            this.Window.AllowUserResizing = true;

            base.Initialize();

            SpriteManager.Camera.BackgroundColor = Color.LightGray;
            SpriteManager.Camera.UsePixelCoordinates();
            GameGlobal.Agent = new NetworkAgent(NetworkAgentRoleEnum.Client, null, 5121);
            FlatRedBall.Screens.ScreenManager.Start(typeof(CharacterSelectionScreen));
        }


        protected override void Update(GameTime gameTime)
        {
            GameGlobal.ProcessPackets();
            FlatRedBallServices.Update(gameTime);
            FlatRedBall.Screens.ScreenManager.Activity();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            FlatRedBallServices.Draw();

            base.Draw(gameTime);
        }
    }
}
