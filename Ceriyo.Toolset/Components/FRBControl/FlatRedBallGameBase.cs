﻿using System;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Threading;
using FlatRedBall;
using FlatRedBall.Graphics;
using FlatRedBall.Input;
using FlatRedBall.Math;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Ceriyo.Toolset.FRBControl
{
    class FlatRedBallGameBase : Game
    {
        private FlatRedBallControl _frbControl;

        #region Constructors

        public FlatRedBallGameBase(FlatRedBallControl frbControl)
        {
            this._frbControl = frbControl;

            // Get the starting size of the control and start listening
            // to its Resize events.
            _windowHandle = frbControl.Handle;
            RenderWidth = (Int32)frbControl.RenderSize.Width;
            RenderHeight = (Int32)frbControl.RenderSize.Height;
            frbControl.SizeChanged += XnaControlOnSizeChanged;

            // Create the graphics device manager and set the delegate for initializing the graphics device
            Graphics = new GraphicsDeviceManager(this);
            Graphics.PreferMultiSampling = true;

            Graphics.SynchronizeWithVerticalRetrace = true;
            Graphics.PreparingDeviceSettings += PreparingDeviceSettings;

            StartInitialization();

            Content.RootDirectory = "Content";
        }

        #endregion

        #region Methods

        protected override void Initialize()
        {
            // Create a timer that will simulate the gameloop
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(1 / 60);
            _timer.Tick += TimerTick;
            _timer.Start();

            // Create the graphics options and initialize FRB with them
            var graphicsOptions = new GraphicsOptions(this, Graphics);
            graphicsOptions.SuspendDeviceReset();
            graphicsOptions.ResolutionWidth = RenderWidth;
            graphicsOptions.ResolutionHeight = RenderHeight;
            graphicsOptions.ResumeDeviceReset();
            FlatRedBallServices.InitializeFlatRedBall(this, Graphics, graphicsOptions);
            Mouse.ModifyMouseState += HandleModifyMouseState;

            base.Initialize();
        }

        private void XnaControlOnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            // Get the new size
            var newWidth = (Int32)e.NewSize.Width;
            var newHeight = (Int32)e.NewSize.Height;

            // Update FRB
            FlatRedBallServices.GraphicsOptions.SetResolution(newWidth, newHeight);
            SpriteManager.Camera.DestinationRectangle = new Rectangle(0, 0, newWidth, newHeight);
            SpriteManager.Camera.UsePixelCoordinates();
        }

        private void StartInitialization()
        {
            Initialize();
        }

        private void PreparingDeviceSettings(object sender, PreparingDeviceSettingsEventArgs e)
        {
            PresentationParameters presentationParams = e.GraphicsDeviceInformation.PresentationParameters;

            presentationParams.BackBufferWidth = RenderWidth;
            presentationParams.BackBufferHeight = RenderHeight;
            presentationParams.DeviceWindowHandle = _windowHandle;
            presentationParams.RenderTargetUsage = RenderTargetUsage.PreserveContents;
        }

        private void TimerTick(object sender, EventArgs e)
        {
            Tick();
        }

        protected override void EndDraw()
        {
            base.EndDraw();

            GraphicsDevice.Present();
        }

        private void HandleModifyMouseState(ref Microsoft.Xna.Framework.Input.MouseState mouseState)
        {
            System.Drawing.Point point = Control.MousePosition;

            var screen = _frbControl.PointFromScreen(new System.Windows.Point(point.X, point.Y));
            var newMouseState = new Microsoft.Xna.Framework.Input.MouseState(
                MathFunctions.RoundToInt(screen.X),
                MathFunctions.RoundToInt(screen.Y),
                mouseState.ScrollWheelValue,
                mouseState.LeftButton,
                mouseState.MiddleButton,
                mouseState.RightButton,
                mouseState.XButton1,
                mouseState.XButton2);
            mouseState = newMouseState;
        }


        #endregion

        #region Fields

        protected readonly GraphicsDeviceManager Graphics;
        protected readonly Int32 RenderHeight;
        protected readonly Int32 RenderWidth;
        private readonly IntPtr _windowHandle;
        protected Boolean IsRenderingPaused;
        private DispatcherTimer _timer;

        #endregion
    }
}
