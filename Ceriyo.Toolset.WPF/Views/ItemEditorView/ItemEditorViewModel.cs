using System.Collections.Generic;
using System.ComponentModel;
using Ceriyo.Core.Data;
using Ceriyo.Core.Extensions;
using Ceriyo.Toolset.WPF.Events;
using Prism.Commands;
using Prism.Events;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;

namespace Ceriyo.Toolset.WPF.Views.ItemEditorView
{
    public class ItemEditorViewModel : BindableBase
    {
        private readonly IEventAggregator _eventAggregator;

        public ItemEditorViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;

            NewCommand = new DelegateCommand(New);
            DeleteCommand = new DelegateCommand(Delete);

            Items = new ObservableCollectionEx<ItemData>();
            Scripts = new Dictionary<string, ScriptData>();
            ItemTypes = new BindingList<ItemTypeData>();

            ConfirmDeleteRequest = new InteractionRequest<IConfirmation>();

            _eventAggregator.GetEvent<DataEditorClosedEvent>().Subscribe(DataEditorClosed);

            Items.ItemPropertyChanged += ItemsOnItemPropertyChanged;
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
                    string globalID = SelectedItem.GlobalID;
                    Items.Remove(SelectedItem);
                    _eventAggregator.GetEvent<ItemDeletedEvent>().Publish(globalID);
                });
        }

        private void DataEditorClosed(bool saveData)
        {
            if (saveData)
            {

            }
        }

        public InteractionRequest<IConfirmation> ConfirmDeleteRequest { get; }

    }
}
