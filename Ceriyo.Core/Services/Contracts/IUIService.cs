using EmptyKeys.UserInterface.Controls;
using Microsoft.Xna.Framework;

namespace Ceriyo.Core.Services.Contracts
{
    public interface IUIService
    {
        void Initialize(IGraphicsDeviceManager graphics);
        void Update(GameTime gameTime);
        void Draw(GameTime gameTime);
        void Exit();

        void ChangeUIRoot<T>(object viewModel)
            where T : UIRoot;
    }
}
