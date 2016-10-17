﻿using System;
using System.IO;
using Ceriyo.Core.Contracts;
using Ceriyo.Core.Data;
using Ceriyo.Domain.Services.Contracts;
using Ceriyo.Domain.Services.DataServices.Contracts;
using Ceriyo.Infrastructure.WPF.Observables;
using Ceriyo.Toolset.WPF.EventArgs;
using Ceriyo.Toolset.WPF.Events.Ability;
using Ceriyo.Toolset.WPF.Events.Class;
using Ceriyo.Toolset.WPF.Events.Creature;
using Ceriyo.Toolset.WPF.Events.DataEditor;
using Ceriyo.Toolset.WPF.Events.Item;
using Ceriyo.Toolset.WPF.Events.Module;
using Ceriyo.Toolset.WPF.Events.Placeable;
using Ceriyo.Toolset.WPF.Events.ResourceEditor;
using Ceriyo.Toolset.WPF.Events.Skill;
using Prism.Events;

namespace Ceriyo.Toolset.WPF
{
    public class DomainServiceNotifier : IDomainServiceNotifier
    {
        private readonly ILogger _logger;
        private readonly IEventAggregator _eventAggregator;
        private readonly IModuleDomainService _moduleDomainService;
        private readonly IDataEditorDomainService _dataEditorDomainService;

        public DomainServiceNotifier(
            ILogger logger,
            IEventAggregator eventAggregator,
            IModuleDomainService moduleDomainService,
            IDataEditorDomainService dataEditorDomainService)
        {
            _logger = logger;
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

            _eventAggregator.GetEvent<AbilityCreatedEvent>().Subscribe(AbilityCreatedOrChanged);
            _eventAggregator.GetEvent<AbilityChangedEvent>().Subscribe(AbilityCreatedOrChanged);
            _eventAggregator.GetEvent<AbilityDeletedEvent>().Subscribe(AbilityDeleted);

            _eventAggregator.GetEvent<ClassCreatedEvent>().Subscribe(ClassCreatedOrChanged);
            _eventAggregator.GetEvent<ClassChangedEvent>().Subscribe(ClassCreatedOrChanged);
            _eventAggregator.GetEvent<ClassDeletedEvent>().Subscribe(ClassDeleted);

            _eventAggregator.GetEvent<CreatureCreatedEvent>().Subscribe(CreatureCreatedOrChanged);
            _eventAggregator.GetEvent<CreatureChangedEvent>().Subscribe(CreatureCreatedOrChanged);
            _eventAggregator.GetEvent<CreatureDeletedEvent>().Subscribe(CreatureDeleted);

            _eventAggregator.GetEvent<ItemCreatedEvent>().Subscribe(ItemCreatedOrChanged);
            _eventAggregator.GetEvent<ItemChangedEvent>().Subscribe(ItemCreatedOrChanged);
            _eventAggregator.GetEvent<ItemDeletedEvent>().Subscribe(ItemDeleted);

            _eventAggregator.GetEvent<PlaceableCreatedEvent>().Subscribe(PlaceableCreatedOrChanged);
            _eventAggregator.GetEvent<PlaceableChangedEvent>().Subscribe(PlaceableCreatedOrChanged);
            _eventAggregator.GetEvent<PlaceableDeletedEvent>().Subscribe(PlaceableDeleted);

            _eventAggregator.GetEvent<SkillCreatedEvent>().Subscribe(SkillCreatedOrChanged);
            _eventAggregator.GetEvent<SkillChangedEvent>().Subscribe(SkillCreatedOrChanged);
            _eventAggregator.GetEvent<SkillDeletedEvent>().Subscribe(SkillDeleted);

            // Resource Editor Events
            _eventAggregator.GetEvent<ResourceEditorClosedEvent>().Subscribe(ResourceEditorClosed);
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
                _logger.Error("Unable to load module", ex);
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
        private void AbilityCreatedOrChanged(AbilityDataObservable data)
        {
            _dataEditorDomainService.AddOrUpdateDirty(data.Observable);
        }
        private void AbilityDeleted(AbilityDataObservable data)
        {
            _dataEditorDomainService.MarkForDeletion(data.Observable);
        }

        // Class Events
        private void ClassCreatedOrChanged(ClassDataObservable data)
        {
            _dataEditorDomainService.AddOrUpdateDirty(data.Observable);
        }
        private void ClassDeleted(ClassDataObservable data)
        {
            _dataEditorDomainService.MarkForDeletion(data.Observable);
        }

        // Creature Events
        private void CreatureCreatedOrChanged(CreatureDataObservable data)
        {
            _dataEditorDomainService.AddOrUpdateDirty(data.Observable);
        }
        private void CreatureDeleted(CreatureDataObservable data)
        {
            _dataEditorDomainService.MarkForDeletion(data.Observable);
        }

        // Item Events
        private void ItemCreatedOrChanged(ItemDataObservable data)
        {
            _dataEditorDomainService.AddOrUpdateDirty(data.Observable);
        }
        private void ItemDeleted(ItemDataObservable data)
        {
            _dataEditorDomainService.MarkForDeletion(data.Observable);
        }


        // Placeable Events
        private void PlaceableCreatedOrChanged(PlaceableDataObservable data)
        {
            _dataEditorDomainService.AddOrUpdateDirty(data.Observable);
        }
        private void PlaceableDeleted(PlaceableDataObservable data)
        {
            _dataEditorDomainService.MarkForDeletion(data.Observable);
        }

        // Skill Events
        private void SkillCreatedOrChanged(SkillDataObservable data)
        {
            _dataEditorDomainService.AddOrUpdateDirty(data.Observable);
        }
        private void SkillDeleted(SkillDataObservable data)
        {
            _dataEditorDomainService.MarkForDeletion(data.Observable);
        }

        #endregion

        #region Resource Editor Events

        private void ResourceEditorClosed()
        {
            
        }

        #endregion
    }
}
