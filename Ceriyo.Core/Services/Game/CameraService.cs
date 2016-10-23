using Ceriyo.Core.Services.Contracts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Ceriyo.Core.Services.Game
{
    public class CameraService: ICameraService
    {
        public float Zoom { get; set; }
        public float Rotation { get; set; }
        public Matrix Transform { get; private set; }
        public Matrix InverseTransform { get; private set; }
        public Vector2 Position { get; set; }
        public Vector2 Origin { get; private set; }
        public Vector2 ScreenCenter { get; private set; }
        private readonly GraphicsDevice _graphics;
        public Vector2 Focus { get; set; }

        public CameraService(GraphicsDevice graphics)
        {
            Transform = new Matrix();
            InverseTransform = new Matrix();
            Position = Vector2.Zero;
            Zoom = 2.5f;
            _graphics = graphics;
        }

        public void Update()
        {
            ScreenCenter = new Vector2(_graphics.Viewport.Width / 2, _graphics.Viewport.Height / 2);
            Origin = ScreenCenter / Zoom;

            Transform = Matrix.Identity *
                    Matrix.CreateTranslation(-Position.X, -Position.Y, 0) *
                    Matrix.CreateRotationZ(Rotation) *
                    Matrix.CreateTranslation(Origin.X, Origin.Y, 0) *
                    Matrix.CreateScale(new Vector3(Zoom, Zoom, Zoom));

            Position = new Vector2(
                Position.X + (Focus.X - Position.X),
                Position.Y + (Focus.Y - Position.Y));

            InverseTransform = Matrix.Invert(Transform);
        }
    }
}
