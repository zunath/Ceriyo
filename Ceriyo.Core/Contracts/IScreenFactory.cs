namespace Ceriyo.Core.Contracts
{
    public interface IScreenFactory
    {
        IScreen Create<T>() where T : IScreen;
    }
}
