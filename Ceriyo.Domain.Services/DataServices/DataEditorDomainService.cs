using System.Collections.Generic;
using Ceriyo.Core.Contracts;
using Ceriyo.Core.Data;
using Ceriyo.Domain.Services.DataServices.Contracts;

namespace Ceriyo.Domain.Services.DataServices
{
    public class DataEditorDomainService: IDataEditorDomainService
    {
        private enum ActionType
        {
            AddOrChanged = 1,
            Delete = 2
        }

        private readonly IDataService _dataService;
        private const string BaseDirectory = "./Modules/temp0/";

        private Dictionary<AbilityData, ActionType> DirtyAbilities { get; set; }
        private Dictionary<ClassData, ActionType> DirtyClasses { get; set; }
        private Dictionary<CreatureData, ActionType> DirtyCreatures { get; set; }
        private Dictionary<ItemData, ActionType> DirtyItems { get; set; }
        private Dictionary<PlaceableData, ActionType> DirtyPlaceables { get; set; }
        private Dictionary<SkillData, ActionType> DirtySkills { get; set; }

        public DataEditorDomainService(IDataService dataService)
        {
            _dataService = dataService;
            DirtyAbilities = new Dictionary<AbilityData, ActionType>();
            DirtyClasses = new Dictionary<ClassData, ActionType>();
            DirtyCreatures = new Dictionary<CreatureData, ActionType>();
            DirtyItems = new Dictionary<ItemData, ActionType>();
            DirtyPlaceables = new Dictionary<PlaceableData, ActionType>();
            DirtySkills = new Dictionary<SkillData, ActionType>();
        }

        public void AddOrUpdateDirty(AbilityData data)
        {
            DirtyAbilities.Remove(data);
            DirtyAbilities.Add(data, ActionType.AddOrChanged);
        }

        public void AddOrUpdateDirty(ClassData data)
        {
            DirtyClasses.Remove(data);
            DirtyClasses.Add(data, ActionType.AddOrChanged);
        }

        public void AddOrUpdateDirty(CreatureData data)
        {
            DirtyCreatures.Remove(data);
            DirtyCreatures.Add(data, ActionType.AddOrChanged);
        }

        public void AddOrUpdateDirty(ItemData data)
        {
            DirtyItems.Remove(data);
            DirtyItems.Add(data, ActionType.AddOrChanged);
        }

        public void AddOrUpdateDirty(PlaceableData data)
        {
            DirtyPlaceables.Remove(data);
            DirtyPlaceables.Add(data, ActionType.AddOrChanged);
        }

        public void AddOrUpdateDirty(SkillData data)
        {
            DirtySkills.Remove(data);
            DirtySkills.Add(data, ActionType.AddOrChanged);
        }

        public void MarkForDeletion(AbilityData data)
        {
            DirtyAbilities.Remove(data);
            DirtyAbilities.Add(data, ActionType.Delete);
        }

        public void MarkForDeletion(ClassData data)
        {
            DirtyClasses.Remove(data);
            DirtyClasses.Add(data, ActionType.Delete);
        }
        public void MarkForDeletion(CreatureData data)
        {
            DirtyCreatures.Remove(data);
            DirtyCreatures.Add(data, ActionType.Delete);
        }
        public void MarkForDeletion(ItemData data)
        {
            DirtyItems.Remove(data);
            DirtyItems.Add(data, ActionType.Delete);
        }
        public void MarkForDeletion(PlaceableData data)
        {
            DirtyPlaceables.Remove(data);
            DirtyPlaceables.Add(data, ActionType.Delete);
        }
        public void MarkForDeletion(SkillData data)
        {
            DirtySkills.Remove(data);
            DirtySkills.Add(data, ActionType.Delete);
        }

        public void ClearQueuedChanges()
        {
            DirtyAbilities.Clear();
            DirtyClasses.Clear();
            DirtyCreatures.Clear();
            DirtyItems.Clear();
            DirtyPlaceables.Clear();
            DirtySkills.Clear();
        }

        public void SaveChanges()
        {
            foreach (var record in DirtyAbilities)
            {
                SaveFile(record.Key, record.Key.Resref, record.Value, "Ability");
            }
            foreach (var record in DirtyClasses)
            {
                SaveFile(record.Key, record.Key.Resref, record.Value, "Class");
            }
            foreach (var record in DirtyCreatures)
            {
                SaveFile(record.Key, record.Key.Resref, record.Value, "Creature");
            }
            foreach (var record in DirtyItems)
            {
                SaveFile(record.Key, record.Key.Resref, record.Value, "Item");
            }
            foreach (var record in DirtyPlaceables)
            {
                SaveFile(record.Key, record.Key.Resref, record.Value, "Placeable");
            }
            foreach (var record in DirtySkills)
            {
                SaveFile(record.Key, record.Key.Resref, record.Value, "Skill");
            }
            
            ClearQueuedChanges();
        }

        private void SaveFile(object obj, string resref, ActionType action, string directoryName)
        {
            if (action == ActionType.AddOrChanged)
                _dataService.Save(obj, $"{BaseDirectory}{directoryName}/{resref}.json");
            else
                _dataService.Delete($"{BaseDirectory}{directoryName}/{resref}.json");
        }

    }
}
