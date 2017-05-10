using Autofac;

namespace Ceriyo.Server.WPF.Contracts
{
    public interface IServerActionFactory
    {
        T Create<T>() where T : IServerAction;
    }
}
