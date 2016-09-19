using Artemis.Interface;

namespace Ceriyo.Core.Contracts
{
    public interface IComponentFactory
    {
        T Create<T>() where T : IComponent;
    }
}
