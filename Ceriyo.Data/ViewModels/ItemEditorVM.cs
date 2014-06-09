﻿using Ceriyo.Data.GameObjects;
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
        private BindingList<LocalVariable> _localVariables;
        private BindingList<GameResource> _graphics;
        private BindingList<Item> _items;
        private BindingList<ItemType> _itemTypes;
        private GameResource _worldGraphic;
        private GameResource _inventoryGraphic;
        private Item _selectedItem;
        private BindingList<ItemProperty> _availableItemProperties;
        private BindingList<ItemProperty> _assignedItemProperties;
        private bool _isItemSelected;

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

        public BindingList<LocalVariable> LocalVariables
        {
            get
            {
                return _localVariables;
            }
            set
            {
                _localVariables = value;
                OnPropertyChanged("LocalVariables");
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

        public BindingList<ItemProperty> AssignedItemProperties
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

        public ItemEditorVM()
        {
            this.LocalVariables = new BindingList<LocalVariable>();
            this.Graphics = new BindingList<GameResource>();
            this.Items = new BindingList<Item>();
            this.ItemTypes = new BindingList<ItemType>();
            this.AvailableItemProperties = new BindingList<ItemProperty>();
            this.AssignedItemProperties = new BindingList<ItemProperty>();
        }
    }
}
