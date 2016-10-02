using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using Ceriyo.Core.Contracts;
using Ceriyo.Core.Data;
using Ceriyo.Toolset.WPF.Events;
using Prism.Events;
using Prism.Interactivity.InteractionRequest;

namespace Ceriyo.Toolset.WPF.Views.OpenModuleView
{
    public class OpenModuleViewModel : BindableBase, IInteractionRequestAware
    {
        private readonly IEventAggregator _eventAggregator;
        private const string ModulesDirectory = "./Modules/";

        public OpenModuleViewModel()
        {
            
        }

        public OpenModuleViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            OpenModuleCommand = new DelegateCommand(OpenModule);
            CancelCommand = new DelegateCommand(Cancel);
            Modules = new BindingList<string>();
            LoadFiles();
        }
        
        private void LoadFiles()
        {
            Modules.Clear();

            foreach (var file in Directory.GetFiles(ModulesDirectory, "*.mod"))
            {
                Modules.Add(Path.GetFileNameWithoutExtension(file));
            }
        }

        private string _selectedModule;

        public string SelectedModule
        {
            get { return _selectedModule; }
            set { SetProperty(ref _selectedModule, value); }
        }

        private BindingList<string> _modules;

        public BindingList<string> Modules
        {
            get { return _modules; }
            set { SetProperty(ref _modules, value); }
        }

        public DelegateCommand OpenModuleCommand { get; set; }

        private void OpenModule()
        {
            if (string.IsNullOrWhiteSpace(SelectedModule)) return;

            _eventAggregator.GetEvent<ModuleOpenedEvent>().Publish(SelectedModule);
            FinishInteraction();
        }

        public DelegateCommand CancelCommand { get; set; }

        private void Cancel()
        {
            FinishInteraction();
        }

        public INotification Notification { get; set; }
        public Action FinishInteraction { get; set; }
    }
}
