using Artemis.Interface;

namespace Ceriyo.Core.Contracts
{
    /// <summary>
    /// Builds components for the Entity-Component-System
    /// </summary>
    public interface IComponentFactory
    {
        /// <summary>
        /// Creates a component for use with the ECS
        /// </summary>
        /// <typeparam name="T">The type of component to create.</typeparam>
        /// <returns></returns>
        T Create<T>() where T : IComponent;
    }
}
