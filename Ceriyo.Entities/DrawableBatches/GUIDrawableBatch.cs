﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FlatRedBall;
using FlatRedBall.Graphics;
using Ceriyo.Library.SquidGUI;
using Squid;
using SampleControls;
using XInput = Microsoft.Xna.Framework.Input;

namespace Ceriyo.Entities.DrawableBatches
{
    public class GUIDrawableBatch : PositionedObject, IDrawableBatch
    {
        private Desktop _desktop;

        public GUIDrawableBatch()
        {
            _desktop = new SampleDesktop { Name = "desk" };
            _desktop.ShowCursor = true;

            InitializeInputManager();
            GuiHost.Renderer = new SquidRendererXNA();

            SpriteManager.AddDrawableBatch(this);
        }

        public void Destroy()
        {
            GuiHost.Renderer = null;
            SpriteManager.RemoveDrawableBatch(this);
        }

        public void Draw(Camera camera)
        {
            GuiHost.TimeElapsed = (float)TimeManager.LastUpdateGameTime.ElapsedGameTime.TotalMilliseconds;

            _desktop.Size = new Squid.Point(FlatRedBallServices.Game.GraphicsDevice.Viewport.Width, 
                FlatRedBallServices.Game.GraphicsDevice.Viewport.Height);
            _desktop.Update();
            _desktop.Draw();

        }

        public void Update()
        {
            XInput.MouseState mouseState = XInput.Mouse.GetState();

            int wheel = mouseState.ScrollWheelValue > LastScroll ? -1 : (mouseState.ScrollWheelValue < LastScroll ? 1 : 0);
            LastScroll = mouseState.ScrollWheelValue;

            Squid.GuiHost.SetMouse(mouseState.X, mouseState.Y, wheel);
            Squid.GuiHost.SetButtons(mouseState.LeftButton == XInput.ButtonState.Pressed, mouseState.RightButton == XInput.ButtonState.Pressed);

            // Keyboard
            XInput.KeyboardState keyboardState = XInput.Keyboard.GetState();
            List<Squid.KeyData> squidKeys = new List<Squid.KeyData>();

            double ms = Squid.GuiHost.TimeElapsed;

            XInput.Keys[] now = keyboardState.GetPressedKeys();
            XInput.Keys[] last = LastKeyboardState.GetPressedKeys();

            foreach (XInput.Keys key in now)
            {
                bool wasDown = LastKeyboardState.IsKeyDown(key);

                InputKeys[key].Repeat -= ms;

                if (InputKeys[key].Repeat < 0 || !wasDown)
                {
                    squidKeys.Add(new Squid.KeyData()
                    {
                        Scancode = InputKeys[key].ScanCode,
                        Pressed = true
                    });
                    InputKeys[key].Repeat = !wasDown ? REPEAT_DELAY : REPEAT_RATE;
                }
            }

            foreach (XInput.Keys key in last)
            {
                bool isDown = keyboardState.IsKeyDown(key);

                if (!isDown)
                {
                    squidKeys.Add(new Squid.KeyData()
                    {
                        Scancode = InputKeys[key].ScanCode,
                        Released = true
                    });
                    InputKeys[key].Repeat = REPEAT_DELAY;
                }
            }

            LastKeyboardState = keyboardState;

            Squid.GuiHost.SetKeyboard(squidKeys.ToArray());
            Squid.GuiHost.TimeElapsed = (float)TimeManager.LastUpdateGameTime.ElapsedGameTime.TotalMilliseconds;
        }

        public bool UpdateEveryFrame
        {
            get { return true; }
        }


        #region SquidUI Input Helpers

        private class InputKey
        {
            public XInput.Keys Key = XInput.Keys.None;
            public int ScanCode = 0;
            public double Repeat = REPEAT_DELAY;
        }

        private static Dictionary<XInput.Keys, int> SpecialKeys = new Dictionary<XInput.Keys, int>();
        private Dictionary<XInput.Keys, InputKey> InputKeys = new Dictionary<XInput.Keys, InputKey>();

        private XInput.KeyboardState LastKeyboardState;
        private int LastScroll;

        private const int REPEAT_DELAY = 500;
        private const int REPEAT_RATE = 25;

        private void InitializeInputManager()
        {
            SpecialKeys.Add(XInput.Keys.Home, 0xC7);
            SpecialKeys.Add(XInput.Keys.Up, 0xC8);
            SpecialKeys.Add(XInput.Keys.Left, 0xCB);
            SpecialKeys.Add(XInput.Keys.Right, 0xCD);
            SpecialKeys.Add(XInput.Keys.End, 0xCF);
            SpecialKeys.Add(XInput.Keys.Down, 0xD0);
            SpecialKeys.Add(XInput.Keys.Insert, 0xD2);
            SpecialKeys.Add(XInput.Keys.Delete, 0xD3);
            SpecialKeys.Add(XInput.Keys.MediaPreviousTrack, 0x90);

            foreach (XInput.Keys k in System.Enum.GetValues(typeof(XInput.Keys)))
            {
                InputKey key = new InputKey();
                key.Key = k;
                key.ScanCode = VirtualKeyToScancode(k);
                InputKeys.Add(k, key);
            }
        }

        private int VirtualKeyToScancode(XInput.Keys key)
        {
            int sc = SquidRendererXNA.VirtualKeyToScancode((int)key);

            if (SpecialKeys.ContainsKey(key))
                sc = SpecialKeys[key];

            return sc;
        }

        #endregion
    }
}
