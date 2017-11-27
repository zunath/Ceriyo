using EmptyKeys.UserInterface.Controls;
using EmptyKeys.UserInterface.Mvvm;
using Microsoft.Xna.Framework;

namespace Ceriyo.Infrastructure.UI.Contracts
{
    public interface IUIService
    {
        void Initialize(IGraphicsDeviceManager graphics);
        void Update(GameTime gameTime);
        void Draw(GameTime gameTime);
        void Exit();

        void ChangeUIRoot<T>(ViewModelBase viewModel)
            where T : UIRoot;
    }
}
