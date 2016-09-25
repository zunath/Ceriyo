using Microsoft.Xna.Framework;

namespace Ceriyo.Core.Services.Contracts
{
    public interface IGameService
    {
        void Initialize(GraphicsDeviceManager graphics);
        void Update(GameTime gameTime);
        void Draw(GameTime gameTime);
        void Exit();
    }
}
