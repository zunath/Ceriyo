using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using Ceriyo.Core.Contracts;
using Ceriyo.Core.Data;
using Ceriyo.Core.Observables;
using Ceriyo.Core.Services.Contracts;
using Ceriyo.Infrastructure.WPF.BindableBases;
using Ceriyo.Infrastructure.WPF.Observables;
using Ceriyo.Toolset.WPF.Events.DataEditor;
using Ceriyo.Toolset.WPF.Events.Item;
using Ceriyo.Toolset.WPF.Events.Module;
using Prism.Commands;
using Prism.Events;
using Prism.Interactivity.InteractionRequest;

namespace Ceriyo.Toolset.WPF.Views.ItemEditorView
{
    public class ItemEditorViewModel : ValidatableBindableBase<ItemEditorViewModel>
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IDataService _dataService;
        private readonly IPathService _pathService;
        private readonly ItemDataObservable.Factory _itemFactory;
        private readonly LocalStringDataObservable.Factory _localStringFactory;
        private readonly LocalDoubleDataObservable.Factory _localDoubleFactory;

        public ItemEditorViewModel(
            IObjectMapper objectMapper,
            ItemEditorViewModelValidator validator,
            IEventAggregator eventAggregator,
            IDataService dataService,
            IPathService pathService,
            ItemDataObservable.Factory itemFactory,
            LocalStringDataObservable.Factory localStringFactory,
            LocalDoubleDataObservable.Factory localDoubleFactory)
            :base(objectMapper, validator)
        {
            _eventAggregator = eventAggregator;
            _dataService = dataService;
            _pathService = pathService;
            _itemFactory = itemFactory;
            _localStringFactory = localStringFactory;
            _localDoubleFactory = localDoubleFactory;

            NewCommand = new DelegateCommand(New);
            DeleteCommand = new DelegateCommand(Delete);

            AddLocalStringCommand = new DelegateCommand(AddLocalString);
            AddLocalDoubleCommand = new DelegateCommand(AddLocalDouble);
            DeleteLocalStringCommand = new DelegateCommand<LocalStringDataObservable>(DeleteLocalString);
            DeleteLocalDoubleCommand = new DelegateCommand<LocalDoubleDataObservable>(DeleteLocalDouble);

            Items = new ObservableCollectionEx<ItemDataObservable>();
            Scripts = new Dictionary<string, ScriptDataObservable>();
            ItemTypes = new ObservableCollectionEx<ItemTypeDataObservable>();
            ItemProperties = new ObservableCollectionEx<ItemPropertyDataObservable>();

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
                ItemData loaded = _dataService.Load<ItemData>(file);
                ItemDataObservable item = _itemFactory.Invoke(loaded);
                Items.Add(item);
            }
        }

        private void RaiseValidityChangedEvent()
        {
            _eventAggregator.GetEvent<ItemEditorValidityChangedEvent>().Publish(!HasErrors);
        }

        private void ItemsOnItemPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            ItemDataObservable itemChanged = (ItemDataObservable)sender;
            _eventAggregator.GetEvent<ItemChangedEvent>().Publish(itemChanged);
            RaiseValidityChangedEvent();
        }

        private ObservableCollectionEx<ItemDataObservable> _items;

        public ObservableCollectionEx<ItemDataObservable> Items
        {
            get { return _items; }
            set { SetProperty(ref _items, value); }
        }

        private ItemDataObservable _selectedItem;
        public ItemDataObservable SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                SetProperty(ref _selectedItem, value);
                OnPropertyChanged("IsItemSelected");
            }
        }
        
        private Dictionary<string, ScriptDataObservable> _scripts;

        public Dictionary<string, ScriptDataObservable> Scripts
        {
            get { return _scripts; }
            set { SetProperty(ref _scripts, value); }
        }

        private ObservableCollectionEx<ItemTypeDataObservable> _itemTypes;

        public ObservableCollectionEx<ItemTypeDataObservable> ItemTypes
        {
            get { return _itemTypes; }
            set { SetProperty(ref _itemTypes, value); }
        }

        private ObservableCollectionEx<ItemPropertyDataObservable> _itemProperties;

        public ObservableCollectionEx<ItemPropertyDataObservable> ItemProperties
        {
            get { return _itemProperties; }
            set { SetProperty(ref _itemProperties, value); }
            
        }

        public bool IsItemSelected => SelectedItem != null;


        public DelegateCommand NewCommand { get; set; }
        public DelegateCommand DeleteCommand { get; set; }

        private void New()
        {
            var item = _itemFactory.Invoke();
            item.Name = "Item" + (Items.Count + 1);
            Items.Add(item);

            _eventAggregator.GetEvent<ItemCreatedEvent>().Publish(item);
            RaiseValidityChangedEvent();
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
                    RaiseValidityChangedEvent();
                });
        }
        
        public DelegateCommand AddLocalStringCommand { get; }

        private void AddLocalString()
        {
            SelectedItem.LocalVariables.LocalStrings.Add(_localStringFactory.Invoke());
        }

        public DelegateCommand<LocalStringDataObservable> DeleteLocalStringCommand { get; }

        private void DeleteLocalString(LocalStringDataObservable localString)
        {
            SelectedItem.LocalVariables.LocalStrings.Remove(localString);
        }

        public DelegateCommand AddLocalDoubleCommand { get; }

        private void AddLocalDouble()
        {
            SelectedItem.LocalVariables.LocalDoubles.Add(_localDoubleFactory.Invoke());
        }

        public DelegateCommand<LocalDoubleDataObservable> DeleteLocalDoubleCommand { get; }

        private void DeleteLocalDouble(LocalDoubleDataObservable localDouble)
        {
            SelectedItem.LocalVariables.LocalDoubles.Remove(localDouble);
        }

        public InteractionRequest<IConfirmation> ConfirmDeleteRequest { get; }
    }
}
