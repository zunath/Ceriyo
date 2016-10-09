using Prism.Commands;
using Prism.Mvvm;
using System;
using System.ComponentModel;
using System.IO;
using System.Windows;
using Ceriyo.Core.Services.Contracts;
using Ceriyo.Toolset.WPF.Events.Module;
using Prism.Events;
using Prism.Interactivity.InteractionRequest;

namespace Ceriyo.Toolset.WPF.Views.OpenModuleView
{
    public class OpenModuleViewModel : BindableBase, IInteractionRequestAware
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IPathService _pathService;
        private readonly FileSystemWatcher _fileSystemWatcher;

        public OpenModuleViewModel()
        {
            
        }

        public OpenModuleViewModel(IEventAggregator eventAggregator,
            IPathService pathService)
        {
            _eventAggregator = eventAggregator;
            _pathService = pathService;
            _fileSystemWatcher = new FileSystemWatcher(_pathService.ModuleDirectory);
            OpenModuleCommand = new DelegateCommand(OpenModule);
            CancelCommand = new DelegateCommand(Cancel);
            Modules = new BindingList<string>();
            LoadFiles();
            _fileSystemWatcher.Created += FileSystemWatcherOnCreated;
            _fileSystemWatcher.Deleted += FileSystemWatcherOnDeleted;
            _fileSystemWatcher.Renamed += FileSystemWatcherOnRenamed;
            _fileSystemWatcher.EnableRaisingEvents = true;
        }

        private void FileSystemWatcherOnRenamed(object sender, RenamedEventArgs e)
        {
            string oldName = Path.GetFileNameWithoutExtension(e.OldName);
            string newName = Path.GetFileNameWithoutExtension(e.Name);
            

            Application.Current.Dispatcher.Invoke(() =>
            {
                if(Path.GetExtension(e.OldName) == ".mod")
                    Modules.Remove(oldName);
                if(Path.GetExtension(e.Name) == ".mod")
                    Modules.Add(newName);
            });
        }

        private void FileSystemWatcherOnDeleted(object sender, FileSystemEventArgs e)
        {
            string name = Path.GetFileNameWithoutExtension(e.Name);
            if (Path.GetExtension(e.Name) != ".mod") return;

            Application.Current.Dispatcher.Invoke(() =>
            {
                Modules.Remove(name);
            });
        }

        private void FileSystemWatcherOnCreated(object sender, FileSystemEventArgs e)
        {
            string name = Path.GetFileNameWithoutExtension(e.Name);
            if (Path.GetExtension(e.Name) != ".mod") return;

            Application.Current.Dispatcher.Invoke(() =>
            {
                Modules.Add(name);
            });
        }
        

        private void LoadFiles()
        {
            Modules.Clear();

            foreach (var file in Directory.GetFiles(_pathService.ModuleDirectory, "*.mod"))
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
