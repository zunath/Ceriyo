using Ceriyo.Data.GameObjects;
using Ceriyo.Data.ResourceObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Ceriyo.Data.ViewModels
{
    public class ItemEditorVM : BaseVM
    {
        private BindingList<GameResource> _graphics;
        private BindingList<Item> _items;
        private BindingList<ItemType> _itemTypes;
        private GameResource _worldGraphic;
        private GameResource _inventoryGraphic;
        private Item _selectedItem;
        private BindingList<ItemProperty> _availableItemProperties;
        private BindingList<AssignedItemProperty> _assignedItemProperties;
        private bool _isItemSelected;
        private BindingList<ItemClassRequirement> _itemRequirements;
        private BindingList<string> _scripts;
        private ItemProperty _selectedAvailableItemProperty;
        private AssignedItemProperty _selectedAssignedItemProperty;

        public BindingList<GameResource> Graphics
        {
            get
            {
                return _graphics;
            }
            set
            {
                _graphics = value;
                OnPropertyChanged("Graphics");
            }
        }

        public GameResource WorldGraphic
        {
            get
            {
                return _worldGraphic;
            }
            set
            {
                _worldGraphic = value;
                OnPropertyChanged("WorldGraphic");
            }
        }

        public GameResource InventoryGraphic
        {
            get
            {
                return _inventoryGraphic;
            }
            set
            {
                _inventoryGraphic = value;
                OnPropertyChanged("InventoryGraphic");
            }
        }

        public Item SelectedItem
        {
            get
            {
                return _selectedItem;
            }
            set
            {
                _selectedItem = value;
                OnPropertyChanged("SelectedItem");
            }
        }

        public BindingList<Item> Items
        {
            get
            {
                return _items;
            }
            set
            {
                _items = value;
                OnPropertyChanged("Items");
            }
        }

        public BindingList<ItemType> ItemTypes
        {
            get
            {
                return _itemTypes;
            }
            set
            {
                _itemTypes = value;
                OnPropertyChanged("ItemTypes");
            }
        }

        public BindingList<ItemClassRequirement> ItemRequirements
        {
            get
            {
                return _itemRequirements;
            }
            set
            {
                _itemRequirements = value;
                OnPropertyChanged("ItemRequirements");
            }
        }

        public BindingList<ItemProperty> AvailableItemProperties
        {
            get
            {
                return _availableItemProperties;
            }
            set
            {
                _availableItemProperties = value;
                OnPropertyChanged("AvailableItemProperties");
            }
        }

        public BindingList<AssignedItemProperty> AssignedItemProperties
        {
            get
            {
                return _assignedItemProperties;
            }
            set
            {
                _assignedItemProperties = value;
                OnPropertyChanged("AssignedItemProperties");
            }
        }

        public bool IsItemSelected
        {
            get
            {
                return _isItemSelected;
            }
            set
            {
                _isItemSelected = value;
                OnPropertyChanged("IsItemSelected");
            }
        }

        public BindingList<string> Scripts
        {
            get
            {
                return _scripts;
            }
            set
            {
                _scripts = value;
                OnPropertyChanged("Scripts");
            }
        }

        public ItemProperty SelectedAvailableItemProperty
        {
            get
            {
                return _selectedAvailableItemProperty;
            }
            set
            {
                _selectedAvailableItemProperty = value;
                OnPropertyChanged("SelectedAvailableItemProperty");
            }
        }

        public AssignedItemProperty SelectedAssignedItemProperty
        {
            get
            {
                return _selectedAssignedItemProperty;
            }
            set
            {
                _selectedAssignedItemProperty = value;
                OnPropertyChanged("SelectedAssignedItemProperty");
            }

        }

        public ItemEditorVM()
        {
            this.Graphics = new BindingList<GameResource>();
            this.Items = new BindingList<Item>();
            this.ItemTypes = new BindingList<ItemType>();
            this.AvailableItemProperties = new BindingList<ItemProperty>();
            this.AssignedItemProperties = new BindingList<AssignedItemProperty>();
            this.ItemRequirements = new BindingList<ItemClassRequirement>();
            this.Scripts = new BindingList<string>();
            this.SelectedAvailableItemProperty = new ItemProperty();
            this.SelectedAssignedItemProperty = new AssignedItemProperty();
        }
    }
}
