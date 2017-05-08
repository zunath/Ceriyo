namespace Ceriyo.Core.Contracts
{
    public interface IUIViewModelFactory
    {
        T Create<T>() where T : IUIViewModel;
    }
}
