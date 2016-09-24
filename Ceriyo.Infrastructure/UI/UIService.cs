using System;
using System.Collections.Generic;
using Ceriyo.Core.Contracts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Squid;
using ButtonState = Microsoft.Xna.Framework.Input.ButtonState;
using Keys = Microsoft.Xna.Framework.Input.Keys;

namespace Ceriyo.Infrastructure.UI
{
    public class UIService: IUIService
    {
        private class InputKey
        {
            public Keys Key = Keys.None;
            public int ScanCode = 0;
            public double Repeat = RepeatDelay;
        }

        private static readonly Dictionary<Keys, int> SpecialKeys = new Dictionary<Keys, int>();
        private readonly Dictionary<Keys, InputKey> _inputKeys = new Dictionary<Keys, InputKey>();

        private KeyboardState _lastKeyboardState;
        private int _lastScroll;

        private const int RepeatDelay = 500;
        private const int RepeatRate = 25;

        private readonly ISquidRenderer _renderer;
        private readonly GraphicsDevice _graphicsDevice;
        private readonly Microsoft.Xna.Framework.Game _game;
        private Desktop _activeDesktop;

        public UIService(ISquidRenderer renderer,
            GraphicsDevice graphicsDevice,
            Microsoft.Xna.Framework.Game game)
        {
            _game = game;
            _renderer = renderer;
            _graphicsDevice = graphicsDevice;
        }

        public void Initialize()
        {
            SpecialKeys.Add(Keys.Home, 0xC7);
            SpecialKeys.Add(Keys.Up, 0xC8);
            SpecialKeys.Add(Keys.Left, 0xCB);
            SpecialKeys.Add(Keys.Right, 0xCD);
            SpecialKeys.Add(Keys.End, 0xCF);
            SpecialKeys.Add(Keys.Down, 0xD0);
            SpecialKeys.Add(Keys.Insert, 0xD2);
            SpecialKeys.Add(Keys.Delete, 0xD3);
            SpecialKeys.Add(Keys.MediaPreviousTrack, 0x90);

            foreach (Keys k in System.Enum.GetValues(typeof(Keys)))
            {
                InputKey key = new InputKey
                {
                    Key = k,
                    ScanCode = VirtualKeyToScancode(k)
                };
                _inputKeys.Add(k, key);
            }

            GuiHost.Renderer = _renderer;
            ChangeDesktop<SampleDesktop>();

            _game.Window.ClientSizeChanged += WindowOnClientSizeChanged;
            _game.IsMouseVisible = true;
        }

        private void WindowOnClientSizeChanged(object sender, EventArgs eventArgs)
        {
            if (_activeDesktop != null)
            {
                _activeDesktop.Size = new Squid.Point(_graphicsDevice.Viewport.Width, _graphicsDevice.Viewport.Height);
            }
        }

        public void ChangeDesktop<T>()
        {
            if(typeof(Desktop).IsSubclassOf(typeof(T)))
                throw new ArgumentException("Type T must inherit from type Desktop.");
            
            _activeDesktop = (Desktop)Activator.CreateInstance(typeof(T));
            _activeDesktop.Size = new Squid.Point(_graphicsDevice.Viewport.Width, _graphicsDevice.Viewport.Height); 
        }

        private static int VirtualKeyToScancode(Keys key)
        {
            int sc = SquidRenderer.VirtualKeyToScancode((int)key);

            if (SpecialKeys.ContainsKey(key))
                sc = SpecialKeys[key];

            return sc;
        }

        public void Update(GameTime gameTime)
        {
            UpdateInput(gameTime);
            _activeDesktop?.Update();
        }

        private void UpdateInput(GameTime gameTime)
        {
            // Mouse
            MouseState mouseState = Mouse.GetState();

            if (mouseState.LeftButton == ButtonState.Pressed)
                System.Diagnostics.Debug.WriteLine("Mouse Clicked.");

            int wheel = mouseState.ScrollWheelValue > _lastScroll ? -1 : (mouseState.ScrollWheelValue < _lastScroll ? 1 : 0);
            _lastScroll = mouseState.ScrollWheelValue;

            Squid.GuiHost.SetMouse(mouseState.X, mouseState.Y, wheel);
            Squid.GuiHost.SetButtons(mouseState.LeftButton == ButtonState.Pressed, mouseState.RightButton == ButtonState.Pressed);

            // Keyboard
            KeyboardState keyboardState = Keyboard.GetState();
            List<Squid.KeyData> squidKeys = new List<Squid.KeyData>();

            double ms = Squid.GuiHost.TimeElapsed;

            Keys[] now = keyboardState.GetPressedKeys();
            Keys[] last = _lastKeyboardState.GetPressedKeys();

            foreach (Keys key in now)
            {
                bool wasDown = _lastKeyboardState.IsKeyDown(key);

                _inputKeys[key].Repeat -= ms;

                if (_inputKeys[key].Repeat < 0 || !wasDown)
                {
                    squidKeys.Add(new Squid.KeyData()
                    {
                        Scancode = _inputKeys[key].ScanCode,
                        Pressed = true
                    });
                    _inputKeys[key].Repeat = !wasDown ? RepeatDelay : RepeatRate;
                }
            }

            foreach (Keys key in last)
            {
                bool isDown = keyboardState.IsKeyDown(key);

                if (!isDown)
                {
                    squidKeys.Add(new Squid.KeyData()
                    {
                        Scancode = _inputKeys[key].ScanCode,
                        Released = true
                    });
                    _inputKeys[key].Repeat = RepeatDelay;
                }
            }

            _lastKeyboardState = keyboardState;

            Squid.GuiHost.SetKeyboard(squidKeys.ToArray());
            Squid.GuiHost.TimeElapsed = (float)gameTime.ElapsedGameTime.TotalMilliseconds;
        }

        public void Draw(GameTime gameTime)
        {
            _activeDesktop?.Draw();
        }

    }
}
