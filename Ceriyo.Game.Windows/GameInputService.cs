using Ceriyo.Core.Services.Contracts;
using Microsoft.Xna.Framework.Input;

namespace Ceriyo.Game.Windows
{
    public class GameInputService: IInputService
    {
        private MouseState _lastMouseState;
        private MouseState _currentMouseState;

        private KeyboardState _lastKeyboardState;
        private KeyboardState _currentKeyboardState;

        public void Update()
        {
            _lastKeyboardState = _currentKeyboardState;
            _lastMouseState = _currentMouseState;

            _currentKeyboardState = Keyboard.GetState();
            _currentMouseState = Mouse.GetState();
        }

        public bool IsLeftMouseDown()
        {
            return _currentMouseState.LeftButton == ButtonState.Pressed;
        }

        public bool IsLeftMouseUp()
        {
            return _currentMouseState.LeftButton == ButtonState.Released;
        }

        public bool IsRightMouseDown()
        {
            return _currentMouseState.RightButton == ButtonState.Pressed;
        }

        public bool IsRightMouseUp()
        {
            return _currentMouseState.RightButton == ButtonState.Released;
        }

        public bool IsLeftMousePressed()
        {
            return _currentMouseState.LeftButton == ButtonState.Released &&
                   _lastMouseState.LeftButton == ButtonState.Pressed;
        }

        public bool IsRightMousePressed()
        {
            return _currentMouseState.RightButton == ButtonState.Released &&
                   _lastMouseState.RightButton == ButtonState.Pressed;
        }
    }
}
