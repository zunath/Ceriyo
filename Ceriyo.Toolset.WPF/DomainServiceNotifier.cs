using System;
using System.IO;
using Ceriyo.Core.Data;
using Ceriyo.Domain.Services.Contracts;
using Ceriyo.Domain.Services.DataServices.Contracts;
using Ceriyo.Toolset.WPF.EventArgs;
using Ceriyo.Toolset.WPF.Events.Ability;
using Ceriyo.Toolset.WPF.Events.Class;
using Ceriyo.Toolset.WPF.Events.Creature;
using Ceriyo.Toolset.WPF.Events.DataEditor;
using Ceriyo.Toolset.WPF.Events.Item;
using Ceriyo.Toolset.WPF.Events.Module;
using Ceriyo.Toolset.WPF.Events.Placeable;
using Ceriyo.Toolset.WPF.Events.Skill;
using Prism.Events;

namespace Ceriyo.Toolset.WPF
{
    public class DomainServiceNotifier : IDomainServiceNotifier
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IModuleDomainService _moduleDomainService;
        private readonly IDataEditorDomainService _dataEditorDomainService;

        public DomainServiceNotifier(
            IEventAggregator eventAggregator,
            IModuleDomainService moduleDomainService,
            IDataEditorDomainService dataEditorDomainService)
        {
            _eventAggregator = eventAggregator;
            _moduleDomainService = moduleDomainService;
            _dataEditorDomainService = dataEditorDomainService;
        }

        public void Initialize()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            // Module Events
            _eventAggregator.GetEvent<ModuleCreatedEvent>().Subscribe(ModuleCreated);
            _eventAggregator.GetEvent<ModuleClosedEvent>().Subscribe(ModuleClosed);
            _eventAggregator.GetEvent<ModulePropertiesChangedEvent>().Subscribe(ModulePropertiesChanged);
            _eventAggregator.GetEvent<ModuleOpenedEvent>().Subscribe(ModuleOpened);
            _eventAggregator.GetEvent<ModuleSavedEvent>().Subscribe(ModuleSaved);

            // Data Editor Events
            _eventAggregator.GetEvent<DataEditorClosedEvent>().Subscribe(DataEditorClosed);

            _eventAggregator.GetEvent<AbilityCreatedEvent>().Subscribe(AbilityCreated);
            _eventAggregator.GetEvent<AbilityChangedEvent>().Subscribe(AbilityChanged);
            _eventAggregator.GetEvent<AbilityDeletedEvent>().Subscribe(AbilityDeleted);

            _eventAggregator.GetEvent<ClassCreatedEvent>().Subscribe(ClassCreated);
            _eventAggregator.GetEvent<ClassChangedEvent>().Subscribe(ClassChanged);
            _eventAggregator.GetEvent<ClassDeletedEvent>().Subscribe(ClassDeleted);

            _eventAggregator.GetEvent<CreatureCreatedEvent>().Subscribe(CreatureCreated);
            _eventAggregator.GetEvent<CreatureChangedEvent>().Subscribe(CreatureChanged);
            _eventAggregator.GetEvent<CreatureDeletedEvent>().Subscribe(CreatureDeleted);

            _eventAggregator.GetEvent<ItemCreatedEvent>().Subscribe(ItemCreated);
            _eventAggregator.GetEvent<ItemChangedEvent>().Subscribe(ItemChanged);
            _eventAggregator.GetEvent<ItemDeletedEvent>().Subscribe(ItemDeleted);

            _eventAggregator.GetEvent<PlaceableCreatedEvent>().Subscribe(PlaceableCreated);
            _eventAggregator.GetEvent<PlaceableChangedEvent>().Subscribe(PlaceableChanged);
            _eventAggregator.GetEvent<PlaceableDeletedEvent>().Subscribe(PlaceableDeleted);

            _eventAggregator.GetEvent<SkillCreatedEvent>().Subscribe(SkillCreated);
            _eventAggregator.GetEvent<SkillChangedEvent>().Subscribe(SkillChanged);
            _eventAggregator.GetEvent<SkillDeletedEvent>().Subscribe(SkillDeleted);
        }


        #region Module Events
        private void ModuleCreated(ModuleEventArgs e)
        {
            _moduleDomainService.CreateModule(e.Name, e.Tag, e.Resref);
        }

        private void ModuleClosed()
        {
            _moduleDomainService.CloseModule();
        }

        private void ModulePropertiesChanged()
        {
            _moduleDomainService.SaveModuleProperties();
        }

        private void ModuleOpened(string fileName)
        {
            try
            {
                _moduleDomainService.OpenModule(fileName);
                _eventAggregator.GetEvent<ModuleLoadedEvent>().Publish(fileName);
            }
            catch (Exception ex)
            {
                throw new FileLoadException("Unable to load module.", ex);
            }
        }

        private void ModuleSaved(string moduleFileName)
        {
            _moduleDomainService.PackModule(moduleFileName);
        }
        #endregion

        #region Data Editor Events
        private void DataEditorClosed(bool doSave)
        {
            if (doSave)
            {
                _dataEditorDomainService.SaveChanges();
            }
            else
            {
                _dataEditorDomainService.ClearQueuedChanges();
            }
        }

        // Ability Events
        private void AbilityCreated(AbilityData data)
        {
            _dataEditorDomainService.AddOrUpdateDirty(data);
        }
        private void AbilityChanged(AbilityData data)
        {
            _dataEditorDomainService.AddOrUpdateDirty(data);
        }
        private void AbilityDeleted(AbilityData data)
        {
            _dataEditorDomainService.MarkForDeletion(data);
        }

        // Class Events
        private void ClassCreated(ClassData data)
        {
            _dataEditorDomainService.AddOrUpdateDirty(data);
        }
        private void ClassChanged(ClassData data)
        {
            _dataEditorDomainService.AddOrUpdateDirty(data);
        }
        private void ClassDeleted(ClassData data)
        {
            _dataEditorDomainService.MarkForDeletion(data);
        }

        // Creature Events
        private void CreatureCreated(CreatureData data)
        {
            _dataEditorDomainService.AddOrUpdateDirty(data);
        }
        private void CreatureChanged(CreatureData data)
        {
            _dataEditorDomainService.AddOrUpdateDirty(data);
        }
        private void CreatureDeleted(CreatureData data)
        {
            _dataEditorDomainService.MarkForDeletion(data);
        }

        // Item Events
        private void ItemCreated(ItemData data)
        {
            _dataEditorDomainService.AddOrUpdateDirty(data);
        }
        private void ItemChanged(ItemData data)
        {
            _dataEditorDomainService.AddOrUpdateDirty(data);
        }
        private void ItemDeleted(ItemData data)
        {
            _dataEditorDomainService.MarkForDeletion(data);
        }


        // Placeable Events
        private void PlaceableCreated(PlaceableData data)
        {
            _dataEditorDomainService.AddOrUpdateDirty(data);
        }
        private void PlaceableChanged(PlaceableData data)
        {
            _dataEditorDomainService.AddOrUpdateDirty(data);
        }
        private void PlaceableDeleted(PlaceableData data)
        {
            _dataEditorDomainService.MarkForDeletion(data);
        }

        // Skill Events
        private void SkillCreated(SkillData data)
        {
            _dataEditorDomainService.AddOrUpdateDirty(data);
        }
        private void SkillChanged(SkillData data)
        {
            _dataEditorDomainService.AddOrUpdateDirty(data);
        }
        private void SkillDeleted(SkillData data)
        {
            _dataEditorDomainService.MarkForDeletion(data);
        }

        #endregion
    }
}
