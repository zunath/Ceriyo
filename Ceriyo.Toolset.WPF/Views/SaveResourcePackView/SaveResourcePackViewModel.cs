using Prism.Commands;
using Prism.Mvvm;
using System;
using System.ComponentModel;
using System.IO;
using System.Windows;
using Ceriyo.Toolset.WPF.Events.ResourceEditor;
using Prism.Interactivity.InteractionRequest;

namespace Ceriyo.Toolset.WPF.Views.SaveResourcePackView
{
    public class SaveResourcePackViewModel : BindableBase, IInteractionRequestAware
    {
        private readonly FileSystemWatcher _fileSystemWatcher;
        private const string ResourcePacksDirectory = "./ResourcePacks/";
        
        public SaveResourcePackViewModel()
        {
            _fileSystemWatcher = new FileSystemWatcher(ResourcePacksDirectory);
            SaveResourcePackCommand = new DelegateCommand(SaveResourcePack);
            CancelCommand = new DelegateCommand(Cancel);
            SaveResourcePackConfirmationRequest = new InteractionRequest<IConfirmation>();

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

        private BindingList<string> _resourcePacks;

        public BindingList<string> ResourcePacks
        {
            get { return _resourcePacks; }
            set { SetProperty(ref _resourcePacks, value); }
        }

        private string _resourcePackName;

        public string ResourcePackName
        {
            get { return _resourcePackName; }
            set { SetProperty(ref _resourcePackName, value); }
        }

        private string _selectedResourcePack;

        public string SelectedResourcePack
        {
            get { return _selectedResourcePack; }
            set
            {
                SetProperty(ref _selectedResourcePack, value);
                ResourcePackName = value;
            }
        }

        public DelegateCommand SaveResourcePackCommand { get; set; }
        public InteractionRequest<IConfirmation> SaveResourcePackConfirmationRequest { get; }

        private void SaveResourcePack()
        {
            if (string.IsNullOrWhiteSpace(ResourcePackName)) return;
            bool doSave = true;

            if (ResourcePacks.Contains(ResourcePackName))
            {
                SaveResourcePackConfirmationRequest.Raise(new Confirmation
                {
                    Title = "Overwrite Resource Pack?",
                    Content = "A resource pack with that name already exists.\n\nAre you sure you want to overwrite it?"
                }, delegate (IConfirmation confirmation)
                {
                    doSave = confirmation.Confirmed;
                });
            }

            if (doSave)
            {
                Notification.Content = ResourcePackName;
                ResourcePackName = string.Empty;
                FinishInteraction();
            }
        }

        public DelegateCommand CancelCommand { get; set; }

        private void Cancel()
        {
            ResourcePackName = string.Empty;
            Notification.Content = null;
            FinishInteraction();
        }

        public INotification Notification { get; set; }
        public Action FinishInteraction { get; set; }
    }
}
