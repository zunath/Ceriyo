using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;
using Ceriyo.Core.Data;
using Ceriyo.Core.Services.Contracts;
using Ceriyo.Toolset.WPF.Events.ManageResources;
using Ceriyo.Toolset.WPF.Events.Module;
using Prism.Commands;
using Prism.Events;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;

namespace Ceriyo.Toolset.WPF.Views.ManageResourcesView
{
    public class ManageResourcesViewModel : BindableBase, IInteractionRequestAware
    {
        private readonly FileSystemWatcher _fileSystemWatcher;
        private readonly IEventAggregator _eventAggregator;
        private readonly IModuleService _domainService;
        private readonly IPathService _pathService;

        public ManageResourcesViewModel(IEventAggregator eventAggregator,
            IModuleService domainService,
            IPathService pathService)
        {
            _eventAggregator = eventAggregator;
            _domainService = domainService;
            _pathService = pathService;
            _fileSystemWatcher = new FileSystemWatcher(_pathService.ResourcePackDirectory);

            ResourcePacks = new BindingList<string>();
            AvailableResourcePacks = new BindingList<string>();

            MoveUpCommand = new DelegateCommand(MoveUp);
            MoveDownCommand = new DelegateCommand(MoveDown);
            RemoveSelectedCommand = new DelegateCommand(RemoveSelected);
            AddPackageCommand = new DelegateCommand(AddPackage);
            SaveChangesCommand = new DelegateCommand(SaveChanges);
            CancelCommand = new DelegateCommand(Cancel);

            _eventAggregator.GetEvent<ManageResourcesClosedEvent>().Subscribe(ManageResourcesClosed);
            _eventAggregator.GetEvent<ModuleLoadedEvent>().Subscribe(ModuleLoaded);
            _eventAggregator.GetEvent<ModuleClosedEvent>().Subscribe(ModuleClosed);

            LoadFiles();
            _fileSystemWatcher.Created += FileSystemWatcherOnCreated;
            _fileSystemWatcher.Deleted += FileSystemWatcherOnDeleted;
            _fileSystemWatcher.Renamed += FileSystemWatcherOnRenamed;
            _fileSystemWatcher.EnableRaisingEvents = true;
        }

        private void ModuleClosed()
        {
            ResourcePacks.Clear();
        }

        private void ModuleLoaded(string fileName)
        {
            LoadData();
        }

        private void LoadData()
        {
            ModuleData moduleData = _domainService.GetLoadedModuleData();
            ResourcePacks.Clear();

            foreach (var pack in moduleData.ResourcePacks)
            {
                ResourcePacks.Add(pack);
            }

            SelectedAvailableResourcePack = AvailableResourcePacks.First();
        }

        private void FileSystemWatcherOnRenamed(object sender, RenamedEventArgs e)
        {
            string oldName = Path.GetFileNameWithoutExtension(e.OldName);
            string newName = Path.GetFileNameWithoutExtension(e.Name);
            
            Application.Current.Dispatcher.Invoke(() =>
            {
                if (Path.GetExtension(e.OldName) == ".rpk")
                    AvailableResourcePacks.Remove(oldName);
                if (Path.GetExtension(e.Name) == ".rpk")
                    AvailableResourcePacks.Add(newName);
            });
        }

        private void FileSystemWatcherOnDeleted(object sender, FileSystemEventArgs e)
        {
            string name = Path.GetFileNameWithoutExtension(e.Name);
            if (Path.GetExtension(e.Name) != ".rpk") return;

            Application.Current.Dispatcher.Invoke(() =>
            {
                AvailableResourcePacks.Remove(name);
            });
        }

        private void FileSystemWatcherOnCreated(object sender, FileSystemEventArgs e)
        {
            string name = Path.GetFileNameWithoutExtension(e.Name);
            if (Path.GetExtension(e.Name) != ".rpk") return;

            Application.Current.Dispatcher.Invoke(() =>
            {
                AvailableResourcePacks.Add(name);
            });
        }
        
        private void LoadFiles()
        {
            AvailableResourcePacks.Clear();
            AvailableResourcePacks.Add(string.Empty);

            foreach (var file in Directory.GetFiles(_pathService.ResourcePackDirectory, "*.rpk"))
            {
                AvailableResourcePacks.Add(Path.GetFileNameWithoutExtension(file));
            }
        }

        private BindingList<string> _resourcePacks;

        public BindingList<string> ResourcePacks
        {
            get { return _resourcePacks; }
            set { SetProperty(ref _resourcePacks, value); }
        }

        private string _selectedResourcePack;

        public string SelectedResourcePack
        {
            get { return _selectedResourcePack; }
            set { SetProperty(ref _selectedResourcePack, value); }
        }

        private BindingList<string> _availableResourcePacks;

        public BindingList<string> AvailableResourcePacks
        {
            get { return _availableResourcePacks; }
            set { SetProperty(ref _availableResourcePacks, value); }
        }

        private string _selectedAvailableResourcePack;

        public string SelectedAvailableResourcePack
        {
            get { return _selectedAvailableResourcePack; }
            set { SetProperty(ref _selectedAvailableResourcePack, value); }
        }

        public DelegateCommand MoveUpCommand { get; set; }

        private void MoveUp()
        {
            if (SelectedResourcePack == null || SelectedResourcePack == ResourcePacks.First()) return;

            int index = ResourcePacks.IndexOf(SelectedResourcePack);
            var temp = ResourcePacks[index];
            ResourcePacks.RemoveAt(index);
            ResourcePacks.Insert(index-1, temp);
            SelectedResourcePack = ResourcePacks[index - 1];
        }

        public DelegateCommand MoveDownCommand { get; set; }

        private void MoveDown()
        {
            if (SelectedResourcePack == null || SelectedResourcePack == ResourcePacks.Last()) return;

            int index = ResourcePacks.IndexOf(SelectedResourcePack);
            var temp = ResourcePacks[index];
            ResourcePacks.RemoveAt(index);
            ResourcePacks.Insert(index+1, temp);
            SelectedResourcePack = ResourcePacks[index + 1];
        }
        public DelegateCommand RemoveSelectedCommand { get; set; }

        private void RemoveSelected()
        {
            if (string.IsNullOrWhiteSpace(SelectedResourcePack)) return;

            int index = ResourcePacks.IndexOf(SelectedResourcePack) - 1;
            ResourcePacks.Remove(SelectedResourcePack);
            if (index < 0) return;
            SelectedResourcePack = ResourcePacks[index];
        }
        public DelegateCommand AddPackageCommand { get; set; }

        private void AddPackage()
        {
            if (string.IsNullOrWhiteSpace(SelectedAvailableResourcePack) ||
                ResourcePacks.Contains(SelectedAvailableResourcePack)) return;

            ResourcePacks.Add(SelectedAvailableResourcePack);
            SelectedResourcePack = ResourcePacks.Last();
        }
        public DelegateCommand SaveChangesCommand { get; set; }

        private void SaveChanges()
        {
            _domainService.ReplaceResourcePacks(ResourcePacks);
            FinishInteraction();
        }
        public DelegateCommand CancelCommand { get; set; }

        private void Cancel()
        {
            FinishInteraction();
        }

        private void ManageResourcesClosed()
        {
            LoadData();
            FinishInteraction();
        }

        public INotification Notification { get; set; }
        public Action FinishInteraction { get; set; }
    }
}
