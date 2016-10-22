using Ceriyo.Core.Data;

namespace Ceriyo.Domain.Services.DataServices.Contracts
{
    public interface IDataEditorDomainService
    {
        void AddOrUpdateDirty(AbilityData data);
        void AddOrUpdateDirty(ClassData data);
        void AddOrUpdateDirty(CreatureData data);
        void AddOrUpdateDirty(ItemData data);
        void AddOrUpdateDirty(PlaceableData data);
        void AddOrUpdateDirty(SkillData data);
        void AddOrUpdateDirty(TilesetData data);

        void MarkForDeletion(AbilityData data);
        void MarkForDeletion(ClassData data);
        void MarkForDeletion(CreatureData data);
        void MarkForDeletion(ItemData data);
        void MarkForDeletion(PlaceableData data);
        void MarkForDeletion(SkillData data);
        void MarkForDeletion(TilesetData data);

        void ClearQueuedChanges();
        void SaveChanges();
    }
}
