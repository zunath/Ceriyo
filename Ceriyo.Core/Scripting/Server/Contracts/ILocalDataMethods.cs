using Artemis;

namespace Ceriyo.Core.Scripting.Server.Contracts
{
    /// <summary>
    /// Scripting methods used for accessing local data.
    /// </summary>
    public interface ILocalDataMethods
    {
        /// <summary>
        /// Sets a local string on an entity.
        /// </summary>
        /// <param name="entity">The entity to set the string onto.</param>
        /// <param name="key">The unique key of the string.</param>
        /// <param name="value">The value to store.</param>
        void SetLocalValue(Entity entity, string key, string value);

        /// <summary>
        /// Sets a local double on an entity.
        /// </summary>
        /// <param name="entity">The entity to set the double onto.</param>
        /// <param name="key">The unique key of the string.</param>
        /// <param name="value">The value to store.</param>
        void SetLocalValue(Entity entity, string key, double value);

        /// <summary>
        /// Gets a local string from an entity.
        /// </summary>
        /// <param name="entity">The entity to get the string from.</param>
        /// <param name="key">The unique key of the string to retrieve.</param>
        /// <returns>The local string value.</returns>
        string GetLocalString(Entity entity, string key);

        /// <summary>
        /// Gets a local double from an entity.
        /// </summary>
        /// <param name="entity">The entity to get the double from.</param>
        /// <param name="key">The unique key of the string to retrieve.</param>
        /// <returns>The local double value.</returns>
        double GetLocalNumber(Entity entity, string key);

        /// <summary>
        /// Deletes a local string from an entity.
        /// </summary>
        /// <param name="entity">The entity to delete the string from.</param>
        /// <param name="key">The unique key of the string to delete.</param>
        void DeleteLocalString(Entity entity, string key);

        /// <summary>
        /// Deletes a local double from an entity.
        /// </summary>
        /// <param name="entity">The entity to delete the double from.</param>
        /// <param name="key">The unique key of the string to delete.</param>
        void DeleteLocalNumber(Entity entity, string key);
    }
}
