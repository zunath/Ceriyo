﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Ceriyo.Data.Enumerations;
using Ceriyo.Data.GameObjects;
using Ceriyo.Data.ResourceObjects;

namespace Ceriyo.Data.ViewModels
{
    public class PaintObjectsVM : BaseVM
    {
        private PaintObjectModeTypeEnum _paintMode;
        private BindingList<Creature> _creatures;
        private BindingList<Item> _items;
        private BindingList<Placeable> _placeables;
        private GameResource _graphic;
        private int _selectedCellX;
        private int _selectedCellY;

        public PaintObjectModeTypeEnum PaintMode 
        {
            get
            {
                return _paintMode;
            }
            set
            {
                _paintMode = value;
                OnPropertyChanged("PaintMode");
            }
        }
        public BindingList<Creature> Creatures 
        {
            get
            {
                return _creatures;
            }
            set
            {
                _creatures = value;
                OnPropertyChanged("Creatures");
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

        public BindingList<Placeable> Placeables
        {
            get
            {
                return _placeables;
            }
            set
            {
                _placeables = value;
                OnPropertyChanged("Placeables");
            }
        }

        public GameResource Graphic
        {
            get
            {
                return _graphic;
            }
            set
            {
                _graphic = value;
                OnPropertyChanged("Graphic");
            }
        }

        public int SelectedCellX
        {
            get
            {
                return _selectedCellX;
            }
            set
            {
                _selectedCellX = value;
                OnPropertyChanged("SelectedCellX");
            }
        }

        public int SelectedCellY
        {
            get
            {
                return _selectedCellY;
            }
            set
            {
                _selectedCellY = value;
                OnPropertyChanged("SelectedCellY");
            }
        }

        public PaintObjectsVM()
        {
            this.PaintMode = PaintObjectModeTypeEnum.None;
            this.Creatures = new BindingList<Creature>();
            this.Items = new BindingList<Item>();
            this.Placeables = new BindingList<Placeable>();
            this.Graphic = new GameResource();
            this.SelectedCellX = 0;
            this.SelectedCellY = 0;
        }
    }
}
