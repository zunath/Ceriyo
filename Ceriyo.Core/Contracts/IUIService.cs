using Microsoft.Xna.Framework;

namespace Ceriyo.Core.Contracts
{
    public interface IUIService
    {
        void Initialize();
        void Update(GameTime gameTime);
        void Draw(GameTime gameTime);
        void ChangeDesktop<T>();
    }
}
