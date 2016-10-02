using System;
using System.ComponentModel;
using System.IO;
using System.Windows;
using Ceriyo.Toolset.WPF.Events.Module;
using Prism.Commands;
using Prism.Events;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;

namespace Ceriyo.Toolset.WPF.Views.SaveModuleView
{
    public class SaveModuleViewModel : BindableBase, IInteractionRequestAware
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly FileSystemWatcher _fileSystemWatcher;
        private const string ModulesDirectory = "./Modules/";

        public SaveModuleViewModel()
        {
            
        }

        public SaveModuleViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _fileSystemWatcher = new FileSystemWatcher(ModulesDirectory);
            SaveModuleCommand = new DelegateCommand(SaveModule);
            CancelCommand = new DelegateCommand(Cancel);
            SaveModuleConfirmationRequest = new InteractionRequest<IConfirmation>();

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
                if (Path.GetExtension(e.OldName) == ".mod")
                    Modules.Remove(oldName);
                if (Path.GetExtension(e.Name) == ".mod")
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

            foreach (var file in Directory.GetFiles(ModulesDirectory, "*.mod"))
            {
                Modules.Add(Path.GetFileNameWithoutExtension(file));
            }
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
        public InteractionRequest<IConfirmation> SaveModuleConfirmationRequest { get; }

        private void SaveModule()
        {
            if (string.IsNullOrWhiteSpace(ModuleName)) return;
            bool doSave = true;

            if (Modules.Contains(ModuleName))
            {
                SaveModuleConfirmationRequest.Raise(new Confirmation
                {
                    Title = "Overwrite Module?",
                    Content = "A module with that name already exists.\n\nAre you sure you want to overwrite it?"
                }, delegate(IConfirmation confirmation)
                {
                    doSave = confirmation.Confirmed;
                });
            }

            if (doSave)
            {
                _eventAggregator.GetEvent<ModuleSavedEvent>().Publish(ModuleName);
                Notification.Content = ModuleName;
                FinishInteraction();
            }
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
