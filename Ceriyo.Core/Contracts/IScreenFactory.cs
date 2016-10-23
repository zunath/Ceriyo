using System;

namespace Ceriyo.Core.Contracts
{
    public interface IScreenFactory
    {
        IScreen Create<T>() where T : IScreen;
        IScreen Create(Type type);
    }
}
