using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Media.Imaging;
using Ceriyo.Core.Constants;
using Ceriyo.Core.Data;
using Ceriyo.Toolset.WPF.Events.Error;
using Microsoft.Win32;
using Prism.Commands;
using Prism.Events;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;

namespace Ceriyo.Toolset.WPF.Views.ResourceEditorView
{
    public class ResourceEditorViewModel : BindableBase
    {
        private enum Tab
        {
            Creatures = 0,
            Icons = 1,
            Items = 2,
            Portraits = 3,
            Tilesets = 4,
            BGM = 5,
            SFX = 6
        }

        private readonly IEventAggregator _eventAggregator;
        private OpenFileDialog _addResourceDialog;

        public ResourceEditorViewModel()
        {

        }

        public ResourceEditorViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;

            _addResourceDialog = new OpenFileDialog
            {
                Multiselect = true
            };

            CreatureResources = new BindingList<ResourceItemData>();
            IconResources = new BindingList<ResourceItemData>();
            ItemResources = new BindingList<ResourceItemData>();
            PortraitResources = new BindingList<ResourceItemData>();
            TilesetResources = new BindingList<ResourceItemData>();
            BGMResources = new BindingList<ResourceItemData>();
            SFXResources = new BindingList<ResourceItemData>();

            NewCommand = new DelegateCommand(New);
            OpenCommand = new DelegateCommand(Open);
            SaveCommand = new DelegateCommand(Save);
            SaveAsCommand = new DelegateCommand(SaveAs);
            ExitCommand = new DelegateCommand(Exit);

            AddResourceCommand = new DelegateCommand(AddResource);
            RemoveResourcesCommand = new DelegateCommand(RemoveResources);

            OpenResourcePackRequest = new InteractionRequest<INotification>();
            DetailedErrorRequest = new InteractionRequest<INotification>();
        }

        private string BuildFilter()
        {
            Tab tab = (Tab) SelectedTabIndex;

            switch (tab)
            {
                case Tab.BGM:
                    return "Audio files (*.mp3) | *.mp3;";
                case Tab.Creatures:
                    return "Image files (*.png) | *.png;";
                case Tab.Icons:
                    return "Image files (*.png) | *.png;";
                case Tab.Items:
                    return "Image files (*.png) | *.png;";
                case Tab.Portraits:
                    return "Image files (*.png) | *.png;";
                case Tab.Tilesets:
                    return "Image files (*.png) | *.png;";
                case Tab.SFX:
                    return "Sound effect files (*.wav) | *.wav;";
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public DelegateCommand NewCommand { get; set; }

        private void New()
        {

        }

        public DelegateCommand OpenCommand { get; set; }
        public InteractionRequest<INotification> OpenResourcePackRequest { get; }

        private void Open()
        {
            OpenResourcePackRequest.Raise(new Notification
            {
                Content = "Open Resource Pack",
                Title = "Open Resource Pack"
            }, delegate(INotification notification)
            {
                // TODO: Load resource pack

            });
        }

        public DelegateCommand SaveCommand { get; set; }

        private void Save()
        {

        }

        public DelegateCommand SaveAsCommand { get; set; }

        private void SaveAs()
        {

        }

        public DelegateCommand ExitCommand { get; set; }

        private void Exit()
        {

        }

        public DelegateCommand AddResourceCommand { get; set; }

        private void AddResource()
        {
            _addResourceDialog.Filter = BuildFilter();
            if (_addResourceDialog.ShowDialog() != true) return;
            List<string> errorFiles = new List<string>();

            foreach (var filePath in _addResourceDialog.FileNames)
            {
                bool isValid;
                try
                {
                    isValid = ValidateResource(filePath);
                }
                catch (Exception)
                {
                    isValid = false;
                }

                if (!isValid)
                {
                    errorFiles.Add(filePath);
                }
                else
                {
                    ResourceItemData item = new ResourceItemData
                    {
                        Extension = Path.GetExtension(filePath),
                        FileName = Path.GetFileNameWithoutExtension(filePath),
                        Size = new FileInfo(filePath).Length,
                        ResourceType = SelectedResourceType
                    };

                    AddOrUpdateResource(item);
                }
            }

            if (errorFiles.Count > 0)
            {
                string detailedError = string.Join(Environment.NewLine, errorFiles);

                RaiseDetailedError("The following files were unable to be added. Please ensure they meet the required dimensions.", detailedError);
            }
        }

        private bool ValidateResource(string filePath)
        {
            string extension = Path.GetExtension(filePath);

            // Audio files are valid based on extension
            switch (SelectedTab)
            {
                case Tab.BGM:
                    return extension == ".mp3";
                case Tab.SFX:
                    return extension == ".wav";
            }

            int height;
            int width;

            // Graphics files must be in specific dimensions for the type of group they're being added to.
            using (var imageStream = File.OpenRead(filePath))
            {
                var decoder = BitmapDecoder.Create(imageStream, BitmapCreateOptions.IgnoreColorProfile,
                    BitmapCacheOption.Default);
                height = decoder.Frames[0].PixelHeight;
                width = decoder.Frames[0].PixelWidth;
            }

            if (height <= 0 || width <= 0) return false;

            if (SelectedTab == Tab.Portraits)
            {
                // Portrait Dimensions: 100x100
                if (width == 100 && height == 100)
                    return true;
            }
            else if (SelectedTab == Tab.Creatures)
            {
                // Creature Dimensions: 128x128 per frame
                if(width % 128 == 0 && height % 128 == 0)
                    return true;
            }
            else if (SelectedTab == Tab.Icons)
            {
                // Icon Dimensions: 60x60 per frame
                if(width % 60 == 0 && height % 60 == 0)
                    return true;
            }
            else if (SelectedTab == Tab.Items)
            {
                // Items dimensions: 128x128 per frame
                if (width % 128 == 0 && height % 128 == 0)
                    return true;
            }
            else if (SelectedTab == Tab.Tilesets)
            {
                // Tilesets dimensions: 64x64 per frame
                if(width % 64 == 0 && height % 64 == 0)
                    return true;
            }

            return false;
        }

        private void AddOrUpdateResource(ResourceItemData item)
        {

            ResourceItemData existing;
            switch (SelectedTab)
            {
                case Tab.BGM:
                    existing = BGMResources.SingleOrDefault(x => x.FileName == item.FileName);
                    if (existing != null) BGMResources.Remove(existing);
                    BGMResources.Add(item);
                    break;
                case Tab.SFX:
                    existing = SFXResources.SingleOrDefault(x => x.FileName == item.FileName);
                    if (existing != null) SFXResources.Remove(existing);
                    SFXResources.Add(item);
                    break;
                case Tab.Creatures:
                    existing = CreatureResources.SingleOrDefault(x => x.FileName == item.FileName);
                    if (existing != null) CreatureResources.Remove(existing);
                    CreatureResources.Add(item);
                    break;
                case Tab.Icons:
                    existing = IconResources.SingleOrDefault(x => x.FileName == item.FileName);
                    if (existing != null) IconResources.Remove(existing);
                    IconResources.Add(item);
                    break;
                case Tab.Items:
                    existing = ItemResources.SingleOrDefault(x => x.FileName == item.FileName);
                    if (existing != null) ItemResources.Remove(existing);
                    ItemResources.Add(item);
                    break;
                case Tab.Portraits:
                    existing = PortraitResources.SingleOrDefault(x => x.FileName == item.FileName);
                    if (existing != null) PortraitResources.Remove(existing);
                    PortraitResources.Add(item);
                    break;
                case Tab.Tilesets:
                    existing = TilesetResources.SingleOrDefault(x => x.FileName == item.FileName);
                    if (existing != null) TilesetResources.Remove(existing);
                    TilesetResources.Add(item);
                    break;
            }
        }

        public DelegateCommand RemoveResourcesCommand { get; set; }

        private void RemoveResources()
        {

        }

        private Tab SelectedTab => (Tab) SelectedTabIndex;

        private ResourceType SelectedResourceType
        {
            get
            {
                switch (SelectedTab)
                {
                    case Tab.BGM: return ResourceType.BGM;
                    case Tab.Creatures: return ResourceType.Creature;
                    case Tab.Icons: return ResourceType.Icon;
                    case Tab.Items: return ResourceType.Item;
                    case Tab.Portraits: return ResourceType.Portrait;
                    case Tab.Tilesets: return ResourceType.Tileset;
                    case Tab.SFX: return ResourceType.SFX;
                    default: throw new ArgumentOutOfRangeException();
                }
            }
        }

        private int _selectedTabIndex;

        public int SelectedTabIndex
        {
            get { return _selectedTabIndex; }
            set { SetProperty(ref _selectedTabIndex, value); }
        }

        private BindingList<ResourceItemData> _creatureResources;

        public BindingList<ResourceItemData> CreatureResources
        {
            get { return _creatureResources; }
            set { SetProperty(ref _creatureResources, value); }
        }
        private BindingList<ResourceItemData> _iconResources;

        public BindingList<ResourceItemData> IconResources
        {
            get { return _iconResources; }
            set { SetProperty(ref _iconResources, value); }
        }
        private BindingList<ResourceItemData> _itemResources;

        public BindingList<ResourceItemData> ItemResources
        {
            get { return _itemResources; }
            set { SetProperty(ref _itemResources, value); }
        }
        private BindingList<ResourceItemData> _portraitResources;

        public BindingList<ResourceItemData> PortraitResources
        {
            get { return _portraitResources; }
            set { SetProperty(ref _portraitResources, value); }
        }
        private BindingList<ResourceItemData> _tilesetResources;

        public BindingList<ResourceItemData> TilesetResources
        {
            get { return _tilesetResources; }
            set { SetProperty(ref _tilesetResources, value); }
        }
        private BindingList<ResourceItemData> _bgmResources;

        public BindingList<ResourceItemData> BGMResources
        {
            get { return _bgmResources; }
            set { SetProperty(ref _bgmResources, value); }
        }
        private BindingList<ResourceItemData> _sfxResources;

        public BindingList<ResourceItemData> SFXResources
        {
            get { return _sfxResources; }
            set { SetProperty(ref _sfxResources, value); }
        }

        
        public InteractionRequest<INotification> DetailedErrorRequest { get; }

        private void RaiseDetailedError(string header, string detailedError)
        {
            _eventAggregator.GetEvent<DetailedErrorEvent>().Publish(new Tuple<string, string>(header, detailedError));

            DetailedErrorRequest.Raise(new Notification
            {
                Title = "Error",
                Content = header
            });
        }
    }
}
