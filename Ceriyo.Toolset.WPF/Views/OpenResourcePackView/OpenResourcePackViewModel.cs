using System;
using System.ComponentModel;
using System.IO;
using System.Windows;
using Prism.Commands;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;

namespace Ceriyo.Toolset.WPF.Views.OpenResourcePackView
{
    public class OpenResourcePackViewModel : BindableBase, IInteractionRequestAware
    {
        private const string ResourcePacksDirectory = "./ResourcePacks/";
        private readonly FileSystemWatcher _fileSystemWatcher;
        
        public OpenResourcePackViewModel()
        {
            _fileSystemWatcher = new FileSystemWatcher(ResourcePacksDirectory);
            OpenResourcePackCommand = new DelegateCommand(OpenResourcePack);
            CancelCommand = new DelegateCommand(Cancel);
            ResourcePacks = new BindingList<string>();
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
                if (Path.GetExtension(e.OldName) == ".rpk")
                    ResourcePacks.Remove(oldName);
                if (Path.GetExtension(e.Name) == ".rpk")
                    ResourcePacks.Add(newName);
            });
        }

        private void FileSystemWatcherOnDeleted(object sender, FileSystemEventArgs e)
        {
            string name = Path.GetFileNameWithoutExtension(e.Name);
            if (Path.GetExtension(e.Name) != ".rpk") return;

            Application.Current.Dispatcher.Invoke(() =>
            {
                ResourcePacks.Remove(name);
            });
        }

        private void FileSystemWatcherOnCreated(object sender, FileSystemEventArgs e)
        {
            string name = Path.GetFileNameWithoutExtension(e.Name);
            if (Path.GetExtension(e.Name) != ".rpk") return;

            Application.Current.Dispatcher.Invoke(() =>
            {
                ResourcePacks.Add(name);
            });
        }


        private void LoadFiles()
        {
            ResourcePacks.Clear();

            foreach (var file in Directory.GetFiles(ResourcePacksDirectory, "*.rpk"))
            {
                ResourcePacks.Add(Path.GetFileNameWithoutExtension(file));
            }
        }

        private string _selectedResourcePack;

        public string SelectedResourcePack
        {
            get { return _selectedResourcePack; }
            set { SetProperty(ref _selectedResourcePack, value); }
        }

        private BindingList<string> _resourcePacks;

        public BindingList<string> ResourcePacks
        {
            get { return _resourcePacks; }
            set { SetProperty(ref _resourcePacks, value); }
        }

        public DelegateCommand OpenResourcePackCommand { get; set; }

        private void OpenResourcePack()
        {
            if (string.IsNullOrWhiteSpace(SelectedResourcePack)) return;

            Notification.Content = SelectedResourcePack;
            FinishInteraction();
        }

        public DelegateCommand CancelCommand { get; set; }

        private void Cancel()
        {
            Notification.Content = null;
            FinishInteraction();
        }

        public INotification Notification { get; set; }
        public Action FinishInteraction { get; set; }

    }
}
