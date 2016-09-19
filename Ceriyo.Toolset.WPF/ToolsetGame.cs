using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ceriyo.Core.Contracts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Framework.WpfInterop;
using MonoGame.Framework.WpfInterop.Input;

namespace Ceriyo.Toolset.WPF
{
    public class ToolsetGame: WpfGame
    {
        private readonly WpfGraphicsDeviceService _graphics;
        private IGameService _gameService;
        private WpfKeyboard _keyboard;
        private WpfMouse _mouse;

        public ToolsetGame()
        {
            _graphics = new WpfGraphicsDeviceService(this);
        }

        protected override void Initialize()
        {

            // wpf and keyboard need reference to the host control in order to receive input
            // this means every WpfGame control will have it's own keyboard & mouse manager which will only react if the mouse is in the control
            _keyboard = new WpfKeyboard(this);
            _mouse = new WpfMouse(this);

            // must be called after the WpfGraphicsDeviceService instance was created
            base.Initialize();
        }

        protected override void Update(GameTime gameTime)
        {
            // every update we can now query the keyboard & mouse for our WpfGame
            var mouseState = _mouse.GetState();
            var keyboardState = _keyboard.GetState();
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            base.Draw(gameTime);
        }
    }
}
