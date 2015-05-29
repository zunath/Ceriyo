using System;
using Ceriyo.Data.Engine;
using Ceriyo.Data.Enumerations;
using Ceriyo.Entities.Screens;
using Ceriyo.Library.Network;
using FlatRedBall;
using FlatRedBall.Screens;
using Microsoft.Xna.Framework;


namespace Ceriyo
{
    public class Game1 : Game
    {
        private readonly GraphicsDeviceManager _graphics;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);

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
            EngineDataManager.InitializeEngine();
			CameraSetup.SetupCamera(SpriteManager.Camera, _graphics);
			GlobalContent.Initialize();
            FlatRedBallServices.IsWindowsCursorVisible = true;
            Window.AllowUserResizing = true;

            base.Initialize();

            SpriteManager.Camera.BackgroundColor = Color.LightGray;
            SpriteManager.Camera.UsePixelCoordinates();
            NetworkManager.Initialize(NetworkAgentRole.Client, 5121); // TODO: Load port from settings
            ScreenManager.Start(typeof(MainMenuScreen));
        }


        protected override void Update(GameTime gameTime)
        {
            NetworkManager.Update();
            FlatRedBallServices.Update(gameTime);
            ScreenManager.Activity();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            FlatRedBallServices.Draw();

            base.Draw(gameTime);
        }

        protected override void OnExiting(object sender, EventArgs args)
        {
            NetworkManager.Shutdown();
            base.OnExiting(sender, args);
        }
    }
}
