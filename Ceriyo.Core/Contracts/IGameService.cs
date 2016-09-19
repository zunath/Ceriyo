using Microsoft.Xna.Framework;

namespace Ceriyo.Core.Contracts
{
    public interface IGameService
    {
        void Initialize(GraphicsDeviceManager graphics);
        void Update(GameTime gameTime);
        void Draw();
        void Exit();
    }
}
