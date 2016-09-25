using Ceriyo.Core.Contracts;

namespace Ceriyo.Core.Services.Contracts
{
    public interface IScreenService
    {
        void ChangeScreen<T>()
            where T : IScreen;
        void Update();
        void Draw();
    }
}
