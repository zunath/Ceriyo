﻿using System;
using System.ComponentModel;
using Ceriyo.Core.Data;
using Ceriyo.Toolset.WPF.EventArgs;
using Ceriyo.Toolset.WPF.Events.Module;
using Prism.Commands;
using Prism.Events;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;

namespace Ceriyo.Toolset.WPF.Views.NewModuleView
{
    public class NewModuleViewModel : BindableBase, IInteractionRequestAware
    {
        private readonly IEventAggregator _eventAggregator;

        public NewModuleViewModel()
        {
        }

        public NewModuleViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            ModuleData = new ModuleData();
            CreateModuleCommand = new DelegateCommand(CreateModule, CanCreateModule);
            CancelCommand = new DelegateCommand(Cancel);

            ModuleData.PropertyChanged += OnPropertyChanged;
        }

        private bool CanCreateModule()
        {
            return !ModuleData.HasErrors;
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            CreateModuleCommand.RaiseCanExecuteChanged();
        }

        private ModuleData _moduleData;

        public ModuleData ModuleData
        {
            get { return _moduleData; }
            set { SetProperty(ref _moduleData, value); }
        }
        
        public DelegateCommand CreateModuleCommand { get; set; }
        private void CreateModule()
        {
            if (ModuleData.HasErrors) return;

            _eventAggregator.GetEvent<ModuleClosedEvent>().Publish();
            _eventAggregator.GetEvent<ModuleCreatedEvent>().Publish(new ModuleEventArgs(ModuleData.Name, ModuleData.Tag, ModuleData.Resref));
            FinishInteraction();
            _eventAggregator.GetEvent<ModuleLoadedEvent>().Publish(null);
            ClearFields();
        }

        public DelegateCommand CancelCommand { get; set; }
        private void Cancel()
        {
            FinishInteraction();
            ClearFields();
        }

        private void ClearFields()
        {
            ModuleData.Name = string.Empty;
            ModuleData.Tag = string.Empty;
            ModuleData.Resref = string.Empty;
        }

        public INotification Notification { get; set; }
        public Action FinishInteraction { get; set; }
    }
}
