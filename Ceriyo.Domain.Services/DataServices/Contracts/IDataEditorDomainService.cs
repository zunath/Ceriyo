using Ceriyo.Core.Data;

namespace Ceriyo.Domain.Services.DataServices.Contracts
{
    /// <summary>
    /// Service used for manipulating dirty object data and marking data for deletion.
    /// </summary>
    public interface IDataEditorDomainService
    {
        /// <summary>
        /// Adds or updates dirty data. Changes won't be persisted until SaveChanges() is called.
        /// </summary>
        /// <param name="data">The data to save.</param>
        void AddOrUpdateDirty(AbilityData data);

        /// <summary>
        /// Adds or updates dirty data. Changes won't be persisted until SaveChanges() is called.
        /// </summary>
        /// <param name="data">The data to save.</param>
        void AddOrUpdateDirty(ClassData data);

        /// <summary>
        /// Adds or updates dirty data. Changes won't be persisted until SaveChanges() is called.
        /// </summary>
        /// <param name="data">The data to save.</param>
        void AddOrUpdateDirty(CreatureData data);

        /// <summary>
        /// Adds or updates dirty data. Changes won't be persisted until SaveChanges() is called.
        /// </summary>
        /// <param name="data">The data to save.</param>
        void AddOrUpdateDirty(ItemData data);

        /// <summary>
        /// Adds or updates dirty data. Changes won't be persisted until SaveChanges() is called.
        /// </summary>
        /// <param name="data">The data to save.</param>
        void AddOrUpdateDirty(PlaceableData data);

        /// <summary>
        /// Adds or updates dirty data. Changes won't be persisted until SaveChanges() is called.
        /// </summary>
        /// <param name="data">The data to save.</param>
        void AddOrUpdateDirty(SkillData data);

        /// <summary>
        /// Adds or updates dirty data. Changes won't be persisted until SaveChanges() is called.
        /// </summary>
        /// <param name="data">The data to save.</param>
        void AddOrUpdateDirty(TilesetData data);

        /// <summary>
        /// Marks data for deletion.
        /// </summary>
        /// <param name="data">The data to mark for deletion</param>
        void MarkForDeletion(AbilityData data);

        /// <summary>
        /// Marks data for deletion.
        /// </summary>
        /// <param name="data">The data to mark for deletion</param>
        void MarkForDeletion(ClassData data);

        /// <summary>
        /// Marks data for deletion.
        /// </summary>
        /// <param name="data">The data to mark for deletion</param>
        void MarkForDeletion(CreatureData data);

        /// <summary>
        /// Marks data for deletion.
        /// </summary>
        /// <param name="data">The data to mark for deletion</param>
        void MarkForDeletion(ItemData data);

        /// <summary>
        /// Marks data for deletion.
        /// </summary>
        /// <param name="data">The data to mark for deletion</param>
        void MarkForDeletion(PlaceableData data);

        /// <summary>
        /// Marks data for deletion.
        /// </summary>
        /// <param name="data">The data to mark for deletion</param>
        void MarkForDeletion(SkillData data);

        /// <summary>
        /// Marks data for deletion.
        /// </summary>
        /// <param name="data">The data to mark for deletion</param>
        void MarkForDeletion(TilesetData data);

        /// <summary>
        /// Clears all queued changes, resetting them to their previous values.
        /// </summary>
        void ClearQueuedChanges();

        /// <summary>
        /// Saves all pending changes.
        /// </summary>
        void SaveChanges();
    }
}
