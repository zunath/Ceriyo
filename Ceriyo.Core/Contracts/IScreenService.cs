namespace Ceriyo.Core.Contracts
{
    public interface IScreenService
    {
        void ChangeScreen<T>()
            where T : IScreen;
        void Update();
        void Draw();
    }
}
