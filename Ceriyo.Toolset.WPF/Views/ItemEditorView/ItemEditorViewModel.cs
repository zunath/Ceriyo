using System.Collections.Generic;
using System.ComponentModel;
using Ceriyo.Core.Data;
using Ceriyo.Core.Observables;
using Ceriyo.Core.Services.Contracts;
using Ceriyo.Infrastructure.WPF.BindableBases;
using Ceriyo.Infrastructure.WPF.Factory.Contracts;
using Ceriyo.Infrastructure.WPF.Observables;
using Ceriyo.Toolset.WPF.Events.DataEditor;
using Ceriyo.Toolset.WPF.Events.Item;
using Ceriyo.Toolset.WPF.Events.Module;
using Prism.Commands;
using Prism.Events;
using Prism.Interactivity.InteractionRequest;

namespace Ceriyo.Toolset.WPF.Views.ItemEditorView
{
    public class ItemEditorViewModel : ValidatableBindableBase<ItemEditorViewModelValidator>
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IObservableDataFactory _observableDataFactory;
        private readonly IModuleDataService _moduleDataService;

        public ItemEditorViewModel(
            IEventAggregator eventAggregator,
            IObservableDataFactory observableDataFactory,
            IModuleDataService moduleDataService)
        {
            _eventAggregator = eventAggregator;
            _observableDataFactory = observableDataFactory;
            _moduleDataService = moduleDataService;

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
            foreach (var loaded in _moduleDataService.LoadAll<ItemData>())
            {
                ItemDataObservable item = _observableDataFactory.CreateAndMap<ItemDataObservable, ItemData>(loaded);
                Items.Add(item);
            }
        }

        private void RaiseValidityChangedEvent()
        {
            ValidateObject();
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
                OnPropertyChanged(nameof(IsItemSelected));
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
            var item = _observableDataFactory.Create<ItemDataObservable>();
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
            SelectedItem.LocalVariables.LocalStrings.Add(_observableDataFactory.Create<LocalStringDataObservable>());
        }

        public DelegateCommand<LocalStringDataObservable> DeleteLocalStringCommand { get; }

        private void DeleteLocalString(LocalStringDataObservable localString)
        {
            SelectedItem.LocalVariables.LocalStrings.Remove(localString);
        }

        public DelegateCommand AddLocalDoubleCommand { get; }

        private void AddLocalDouble()
        {
            SelectedItem.LocalVariables.LocalDoubles.Add(_observableDataFactory.Create<LocalDoubleDataObservable>());
        }

        public DelegateCommand<LocalDoubleDataObservable> DeleteLocalDoubleCommand { get; }

        private void DeleteLocalDouble(LocalDoubleDataObservable localDouble)
        {
            SelectedItem.LocalVariables.LocalDoubles.Remove(localDouble);
        }

        public InteractionRequest<IConfirmation> ConfirmDeleteRequest { get; }
    }
}
