using System.ComponentModel;
using Ceriyo.Core.Data;
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

        public ResourceEditorViewModel()
        {
            
        }

        public ResourceEditorViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;

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
            
        }

        public DelegateCommand RemoveResourcesCommand { get; set; }

        private void RemoveResources()
        {

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

        

    }
}
