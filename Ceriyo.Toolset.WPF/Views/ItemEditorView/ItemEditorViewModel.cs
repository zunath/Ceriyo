using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using Ceriyo.Core.Contracts;
using Ceriyo.Core.Data;
using Ceriyo.Core.Observables;
using Ceriyo.Core.Services.Contracts;
using Ceriyo.Infrastructure.WPF.BindableBases;
using Ceriyo.Toolset.WPF.Events.DataEditor;
using Ceriyo.Toolset.WPF.Events.Item;
using Ceriyo.Toolset.WPF.Events.Module;
using FluentValidation;
using Prism.Commands;
using Prism.Events;
using Prism.Interactivity.InteractionRequest;

namespace Ceriyo.Toolset.WPF.Views.ItemEditorView
{
    public class ItemEditorViewModel : ValidatableBindableBase
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IDataService _dataService;
        private readonly IPathService _pathService;

        public ItemEditorViewModel(IEventAggregator eventAggregator,
            IDataService dataService,
            IPathService pathService)
        {
            _eventAggregator = eventAggregator;
            _dataService = dataService;
            _pathService = pathService;
            
            NewCommand = new DelegateCommand(New);
            DeleteCommand = new DelegateCommand(Delete);

            Items = new ObservableCollectionEx<ItemData>();
            Scripts = new Dictionary<string, ScriptData>();
            ItemTypes = new BindingList<ItemTypeData>();

            ConfirmDeleteRequest = new InteractionRequest<IConfirmation>();
            
            Items.ItemPropertyChanged += ItemsOnItemPropertyChanged;

            _eventAggregator.GetEvent<ModuleLoadedEvent>().Subscribe(ModuleLoaded);
            _eventAggregator.GetEvent<DataEditorClosedEvent>().Subscribe(DataEditorClosed);
            _eventAggregator.GetEvent<ModuleClosedEvent>().Subscribe(ModuleClosed);
        }


        private void ModuleLoaded(string moduleFileName)
        {
            LoadExistingData();
        }

        private void ModuleClosed()
        {
            Items.Clear();
        }

        private void DataEditorClosed(bool doSave)
        {
            LoadExistingData();
        }

        private void LoadExistingData()
        {
            Items.Clear();
            string[] files = Directory.GetFiles($"{_pathService.ModulesTempDirectory}Item/", "*.dat");

            foreach (var file in files)
            {
                Items.Add(_dataService.Load<ItemData>(file));
            }
        }

        private void ItemsOnItemPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            ItemData itemChanged = sender as ItemData;
            _eventAggregator.GetEvent<ItemChangedEvent>().Publish(itemChanged);
        }

        private ObservableCollectionEx<ItemData> _items;

        public ObservableCollectionEx<ItemData> Items
        {
            get { return _items; }
            set { SetProperty(ref _items, value); }
        }

        private ItemData _selectedItem;
        public ItemData SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                SetProperty(ref _selectedItem, value);
                OnPropertyChanged("IsItemSelected");
            }
        }

        private Dictionary<string, ScriptData> _scripts;

        public Dictionary<string, ScriptData> Scripts
        {
            get { return _scripts; }
            set { SetProperty(ref _scripts, value); }
        }

        private BindingList<ItemTypeData> _itemTypes;

        public BindingList<ItemTypeData> ItemTypes
        {
            get { return _itemTypes; }
            set { SetProperty(ref _itemTypes, value); }
        }

        private BindingList<ItemPropertyData> _itemProperties;

        public BindingList<ItemPropertyData> ItemProperties
        {
            get { return _itemProperties; }
            set { SetProperty(ref _itemProperties, value); }
            
        }

        public bool IsItemSelected => SelectedItem != null;


        public DelegateCommand NewCommand { get; set; }
        public DelegateCommand DeleteCommand { get; set; }

        private void New()
        {
            ItemData item = new ItemData
            {
                Name = "Item" + (Items.Count + 1)
            };
            Items.Add(item);

            _eventAggregator.GetEvent<ItemCreatedEvent>().Publish(item);
        }

        private void Delete()
        {
            ConfirmDeleteRequest.Raise(
                new Confirmation
                {
                    Title = "Delete Item?",
                    Content = "Are you sure you want to delete this item?"
                }, c =>
                {
                    if (!c.Confirmed) return;
                    _eventAggregator.GetEvent<ItemDeletedEvent>().Publish(SelectedItem);
                    Items.Remove(SelectedItem);
                });
        }
        
        public InteractionRequest<IConfirmation> ConfirmDeleteRequest { get; }

        private IValidator _validator;
        protected override IValidator Validator => _validator ?? (_validator = new ItemEditorViewModelValidator());
    }
}
