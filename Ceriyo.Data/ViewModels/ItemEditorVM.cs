using System.ComponentModel;
using Ceriyo.Data.GameObjects;
using Ceriyo.Data.ResourceObjects;

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
        private BindingList<CharacterClass> _characterClasses; 

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

        public BindingList<CharacterClass> CharacterClasses
        {
            get
            {
                return _characterClasses; 
            }
            set
            {
                _characterClasses = value;
                OnPropertyChanged("CharacterClasses");
            }
        }

        public ItemEditorVM()
        {
            Graphics = new BindingList<GameResource>();
            Items = new BindingList<Item>();
            ItemTypes = new BindingList<ItemType>();
            AvailableItemProperties = new BindingList<ItemProperty>();
            AssignedItemProperties = new BindingList<AssignedItemProperty>();
            ItemRequirements = new BindingList<ItemClassRequirement>();
            Scripts = new BindingList<string>();
            SelectedAvailableItemProperty = new ItemProperty();
            SelectedAssignedItemProperty = new AssignedItemProperty();
            CharacterClasses = new BindingList<CharacterClass>();
        }
    }
}
