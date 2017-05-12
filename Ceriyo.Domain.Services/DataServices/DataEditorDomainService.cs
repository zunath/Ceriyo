using System;
using System.Collections.Generic;
using Ceriyo.Core.Contracts;
using Ceriyo.Core.Data;
using Ceriyo.Core.Services.Contracts;
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

        private class DirtyGroup<T>: Dictionary<string, Tuple<T, ActionType>>
        {
            public void Add(string globalID, T obj, ActionType action)
            {
                Add(globalID, new Tuple<T, ActionType>(obj, action));
            }
        }

        private readonly IDataService _dataService;
        private readonly IPathService _pathService;

        private DirtyGroup<AbilityData> DirtyAbilities { get; }
        private DirtyGroup<ClassData> DirtyClasses { get; }
        private DirtyGroup<CreatureData> DirtyCreatures { get; }
        private DirtyGroup<ItemData> DirtyItems { get; }
        private DirtyGroup<PlaceableData> DirtyPlaceables { get; }
        private DirtyGroup<SkillData> DirtySkills { get; }

        private DirtyGroup<TilesetData> DirtyTilesets { get; }
        public DataEditorDomainService(IDataService dataService,
            IPathService pathService)
        {
            _dataService = dataService;
            _pathService = pathService;
            DirtyAbilities = new DirtyGroup<AbilityData>();
            DirtyClasses = new DirtyGroup<ClassData>();
            DirtyCreatures = new DirtyGroup<CreatureData>();
            DirtyItems = new DirtyGroup<ItemData>();
            DirtyPlaceables = new DirtyGroup<PlaceableData>();
            DirtySkills = new DirtyGroup<SkillData>();
            DirtyTilesets = new DirtyGroup<TilesetData>();
        }

        public void AddOrUpdateDirty(AbilityData data)
        {
            DirtyAbilities.Remove(data.GlobalID);
            DirtyAbilities.Add(data.GlobalID, data, ActionType.AddOrChanged);
        }

        public void AddOrUpdateDirty(ClassData data)
        {
            DirtyClasses.Remove(data.GlobalID);
            DirtyClasses.Add(data.GlobalID, data, ActionType.AddOrChanged);
        }

        public void AddOrUpdateDirty(CreatureData data)
        {
            DirtyCreatures.Remove(data.GlobalID);
            DirtyCreatures.Add(data.GlobalID, data, ActionType.AddOrChanged);
        }

        public void AddOrUpdateDirty(ItemData data)
        {
            DirtyItems.Remove(data.GlobalID);
            DirtyItems.Add(data.GlobalID, data, ActionType.AddOrChanged);
        }

        public void AddOrUpdateDirty(PlaceableData data)
        {
            DirtyPlaceables.Remove(data.GlobalID);
            DirtyPlaceables.Add(data.GlobalID, data, ActionType.AddOrChanged);
        }

        public void AddOrUpdateDirty(SkillData data)
        {
            DirtySkills.Remove(data.GlobalID);
            DirtySkills.Add(data.GlobalID, data, ActionType.AddOrChanged);
        }

        public void AddOrUpdateDirty(TilesetData data)
        {
            DirtyTilesets.Remove(data.GlobalID);
            DirtyTilesets.Add(data.GlobalID, data, ActionType.AddOrChanged);
        }

        public void MarkForDeletion(AbilityData data)
        {
            DirtyAbilities.Remove(data.GlobalID);
            DirtyAbilities.Add(data.GlobalID, data, ActionType.Delete);
        }

        public void MarkForDeletion(ClassData data)
        {
            DirtyClasses.Remove(data.GlobalID);
            DirtyClasses.Add(data.GlobalID, data, ActionType.Delete);
        }
        public void MarkForDeletion(CreatureData data)
        {
            DirtyCreatures.Remove(data.GlobalID);
            DirtyCreatures.Add(data.GlobalID, data, ActionType.Delete);
        }
        public void MarkForDeletion(ItemData data)
        {
            DirtyItems.Remove(data.GlobalID);
            DirtyItems.Add(data.GlobalID, data, ActionType.Delete);
        }
        public void MarkForDeletion(PlaceableData data)
        {
            DirtyPlaceables.Remove(data.GlobalID);
            DirtyPlaceables.Add(data.GlobalID, data, ActionType.Delete);
        }
        public void MarkForDeletion(SkillData data)
        {
            DirtySkills.Remove(data.GlobalID);
            DirtySkills.Add(data.GlobalID, data, ActionType.Delete);
        }

        public void MarkForDeletion(TilesetData data)
        {
            DirtyTilesets.Remove(data.GlobalID);
            DirtyTilesets.Add(data.GlobalID, data, ActionType.Delete);
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
                SaveOrDeleteFile(record.Value.Item1, record.Key, record.Value.Item2, "Ability");
            }
            foreach (var record in DirtyClasses)
            {
                SaveOrDeleteFile(record.Value.Item1, record.Key, record.Value.Item2, "Class");
            }
            foreach (var record in DirtyCreatures)
            {
                SaveOrDeleteFile(record.Value.Item1, record.Key, record.Value.Item2, "Creature");
            }
            foreach (var record in DirtyItems)
            {
                SaveOrDeleteFile(record.Value.Item1, record.Key, record.Value.Item2, "Item");
            }
            foreach (var record in DirtyPlaceables)
            {
                SaveOrDeleteFile(record.Value.Item1, record.Key, record.Value.Item2, "Placeable");
            }
            foreach (var record in DirtySkills)
            {
                SaveOrDeleteFile(record.Value.Item1, record.Key, record.Value.Item2, "Skill");
            }
            foreach (var record in DirtyTilesets)
            {
                SaveOrDeleteFile(record.Value.Item1, record.Key, record.Value.Item2, "Tileset");
            }
            
            ClearQueuedChanges();
        }

        private void SaveOrDeleteFile(object obj, string globalID, ActionType action, string directoryName)
        {
            if (action == ActionType.AddOrChanged)
                _dataService.Save(obj, $"{_pathService.ModulesToolsetTempDirectory}{directoryName}/{globalID}.dat");
            else
                _dataService.Delete($"{_pathService.ModulesToolsetTempDirectory}{directoryName}/{globalID}.dat");
        }

    }
}
