using Ceriyo.Core.Services.Contracts;
using Ceriyo.Infrastructure.WPF.MonoGameWpfInterop.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Ceriyo.Toolset.WPF.GameWorld
{
    public class ToolsetInputService: IInputService
    {
        private readonly WpfMouse _mouse;
        private readonly WpfKeyboard _keyboard;

        private MouseState _lastMouseState;
        private MouseState _currentMouseState;

        private KeyboardState _lastKeyboardState;
        private KeyboardState _currentKeyboardState;

        public ToolsetInputService(ToolsetGame game)
        {
            _mouse = new WpfMouse(game);
            _keyboard = new WpfKeyboard(game);
        }

        public void Update()
        {
            _lastKeyboardState = _currentKeyboardState;
            _lastMouseState = _currentMouseState;

            _currentKeyboardState = _keyboard.GetState();
            _currentMouseState = _mouse.GetState();
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

        public Vector2 GetMousePosition()
        {
            return _currentMouseState.Position.ToVector2();
        }
    }
}
