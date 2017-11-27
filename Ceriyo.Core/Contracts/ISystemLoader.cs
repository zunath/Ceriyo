namespace Ceriyo.Core.Contracts
{
    /// <summary>
    /// Loads systems for use in the Entity-Component-System
    /// </summary>
    public interface ISystemLoader
    {
        /// <summary>
        /// Loads systems used in the ECS
        /// </summary>
        void LoadSystems();
    }
}
