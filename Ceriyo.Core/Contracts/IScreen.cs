namespace Ceriyo.Core.Contracts
{
    public interface IScreen
    {
        void Initialize();
        void Update();
        void Draw();
        void Close();
    }
}
