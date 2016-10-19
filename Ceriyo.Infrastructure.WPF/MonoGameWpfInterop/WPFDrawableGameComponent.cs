using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Ceriyo.Infrastructure.WPF.MonoGameWpfInterop
{
    public class WpfDrawableGameComponent : WpfGameComponent, IDrawable
    {
        #region Fields

        private readonly WpfGame _game;
        private bool _visible = true;
        private int _drawOrder;
        private bool _initialized;

        #endregion

        #region Constructors

        public WpfDrawableGameComponent(WpfGame game) : base(game)
        {
            _game = game;
        }

        #endregion

        #region Events

        public event EventHandler<EventArgs> DrawOrderChanged;

        public event EventHandler<EventArgs> VisibleChanged;

        #endregion

        #region Properties

        public GraphicsDevice GraphicsDevice => _game.GraphicsDevice;

        public int DrawOrder
        {
            get { return _drawOrder; }
            set
            {
                if (_drawOrder == value)
                    return;
                _drawOrder = value;
                var ev = DrawOrderChanged;
                ev?.Invoke(this, EventArgs.Empty);
            }
        }

        public bool Visible
        {
            get { return _visible; }
            set
            {
                if (_visible == value)
                    return;
                _visible = value;
                var ev = VisibleChanged;
                ev?.Invoke(this, EventArgs.Empty);
            }
        }

        #endregion

        #region Methods

        public override void Initialize()
        {
            base.Initialize();
            if (!_initialized)
            {
                _initialized = true;
                LoadContent();
            }
        }

        public virtual void Draw(GameTime gameTime) { }

        protected virtual void LoadContent() { }

        #endregion
    }
}