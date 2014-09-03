﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FlatRedBall;
using FlatRedBall.Graphics;
using Ceriyo.Library.SquidGUI;
using Squid;
using XInput = Microsoft.Xna.Framework.Input;
using FlatRedBall.Input;
using Ceriyo.Data.ResourceObjects;

namespace Ceriyo.Entities.DrawableBatches
{
    public abstract class GUIDrawableBatch : PositionedObject, IDrawableBatch
    {
        protected Desktop _desktop;
        private SquidLayoutManager _layoutManager;
        private Layer _uiLayer;
        private string _layoutName;

        public GUIDrawableBatch(string layoutName)
        {
            _layoutName = layoutName;
            _layoutManager = new SquidLayoutManager();

            _desktop = _layoutManager.LayoutToDesktop(layoutName);
            _desktop.ShowCursor = false;

            InitializeInputManager();
            GuiHost.Renderer = new SquidRendererXNA();

            _uiLayer = SpriteManager.AddLayer();

            _desktop.Size = new Squid.Point(FlatRedBallServices.Game.GraphicsDevice.Viewport.Width,
                FlatRedBallServices.Game.GraphicsDevice.Viewport.Height);
            FlatRedBallServices.Game.Window.ClientSizeChanged += Window_ClientSizeChanged;

            SpriteManager.AddToLayer(this, _uiLayer);
        }

        private void Window_ClientSizeChanged(object sender, EventArgs e)
        {
            _desktop.Size = new Squid.Point(FlatRedBallServices.Game.GraphicsDevice.Viewport.Width,
                FlatRedBallServices.Game.GraphicsDevice.Viewport.Height);

            foreach (Control control in _desktop.Controls)
            {
                UIComponent component = control.UserData as UIComponent;
                int centerX = FlatRedBallServices.Game.GraphicsDevice.Viewport.Width / 2;
                int centerY = FlatRedBallServices.Game.GraphicsDevice.Viewport.Height / 2;
                int windowCenterX = control.Size.x / 2;
                int windowCenterY = control.Size.y / 2;

                control.Position = new Point(component.PositionX + centerX - windowCenterX,
                                             component.PositionY + centerY - windowCenterY);

            }


        }

        public void Destroy()
        {
            GuiHost.Renderer = null;
            
            _uiLayer.Remove(this);
            SpriteManager.RemoveLayer(_uiLayer);
        }

        public void Draw(Camera camera)
        {
            GuiHost.TimeElapsed = (float)TimeManager.LastUpdateGameTime.ElapsedGameTime.TotalMilliseconds;

            _desktop.Update();
            _desktop.Draw();

        }

        public void Update()
        {
            XInput.MouseState mouseState = XInput.Mouse.GetState();

            int wheel = InputManager.Mouse.ScrollWheel < 0 ? 1 : (InputManager.Mouse.ScrollWheel > 0 ? -1 : 0);
            
            Squid.GuiHost.SetMouse(InputManager.Mouse.X, InputManager.Mouse.Y, wheel);
            Squid.GuiHost.SetButtons(mouseState.LeftButton == XInput.ButtonState.Pressed, mouseState.RightButton == XInput.ButtonState.Pressed);

            // Keyboard
            XInput.KeyboardState keyboardState = XInput.Keyboard.GetState();
            _squidKeys.Clear();

            double ms = Squid.GuiHost.TimeElapsed;

            XInput.Keys[] now = keyboardState.GetPressedKeys();
            XInput.Keys[] last = LastKeyboardState.GetPressedKeys();

            foreach (XInput.Keys key in now)
            {
                bool wasDown = LastKeyboardState.IsKeyDown(key);

                InputKeys[key].Repeat -= ms;

                if (InputKeys[key].Repeat < 0 || !wasDown)
                {
                    _squidKeys.Add(new Squid.KeyData()
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
                    _squidKeys.Add(new Squid.KeyData()
                    {
                        Scancode = InputKeys[key].ScanCode,
                        Released = true
                    });
                    InputKeys[key].Repeat = REPEAT_DELAY;
                }
            }

            LastKeyboardState = keyboardState;

            Squid.GuiHost.SetKeyboard(_squidKeys.ToArray());
            Squid.GuiHost.TimeElapsed = (float)TimeManager.LastUpdateGameTime.ElapsedGameTime.TotalMilliseconds;
        }

        public bool UpdateEveryFrame
        {
            get { return true; }
        }


        public Control GetControl(string controlName)
        {
            return _desktop.GetControl(controlName);
        }

        #region SquidUI Input Helpers

        private class InputKey
        {
            public XInput.Keys Key = XInput.Keys.None;
            public int ScanCode = 0;
            public double Repeat = REPEAT_DELAY;
        }

        private Dictionary<XInput.Keys, int> SpecialKeys = new Dictionary<XInput.Keys, int>();
        private Dictionary<XInput.Keys, InputKey> InputKeys = new Dictionary<XInput.Keys, InputKey>();
        private List<Squid.KeyData> _squidKeys = new List<KeyData>();

        private XInput.KeyboardState LastKeyboardState;

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
