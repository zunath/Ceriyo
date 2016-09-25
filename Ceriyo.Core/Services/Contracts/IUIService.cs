using Microsoft.Xna.Framework;
using Squid;

namespace Ceriyo.Core.Services.Contracts
{
    public interface IUIService
    {
        void Initialize();
        void Update(GameTime gameTime);
        void Draw(GameTime gameTime);
        void ChangeDesktop<T>()
            where T: Desktop;
        void ChangeDesktop(Desktop desktop);
    }
}
