namespace Ceriyo.Core.Contracts
{
    /// <summary>
    /// Creates UI View Models
    /// </summary>
    public interface IUIViewModelFactory
    {
        /// <summary>
        /// Create a UI view model.
        /// </summary>
        /// <typeparam name="T">The type of view model to create.</typeparam>
        /// <returns>The created view model.</returns>
        T Create<T>() where T : IUIViewModel;
    }
}
