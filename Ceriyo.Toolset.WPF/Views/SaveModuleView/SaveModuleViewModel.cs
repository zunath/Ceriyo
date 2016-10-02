using System;
using System.ComponentModel;
using Ceriyo.Toolset.WPF.Events;
using Prism.Commands;
using Prism.Events;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;

namespace Ceriyo.Toolset.WPF.Views.SaveModuleView
{
    public class SaveModuleViewModel : BindableBase, IInteractionRequestAware
    {
        private readonly IEventAggregator _eventAggregator;

        public SaveModuleViewModel()
        {
            
        }

        public SaveModuleViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            SaveModuleCommand = new DelegateCommand(SaveModule);
            CancelCommand = new DelegateCommand(Cancel);
        }

        private BindingList<string> _modules;

        public BindingList<string> Modules
        {
            get { return _modules; }
            set { SetProperty(ref _modules, value); }
        }

        private string _moduleName;

        public string ModuleName
        {
            get { return _moduleName; }
            set { SetProperty(ref _moduleName, value); }
        }

        private string _selectedModule;

        public string SelectedModule
        {
            get { return _selectedModule; }
            set
            {
                SetProperty(ref _selectedModule, value);
                ModuleName = value;
            }
        }

        public DelegateCommand SaveModuleCommand { get; set; }

        private void SaveModule()
        {
            if (string.IsNullOrWhiteSpace(ModuleName)) return;

            _eventAggregator.GetEvent<ModuleSavedEvent>().Publish(ModuleName);
            Notification.Content = ModuleName;
            FinishInteraction();
        }

        public DelegateCommand CancelCommand { get; set; }

        private void Cancel()
        {
            ModuleName = string.Empty;
            FinishInteraction();
        }

        public INotification Notification { get; set; }
        public Action FinishInteraction { get; set; }
    }
}
