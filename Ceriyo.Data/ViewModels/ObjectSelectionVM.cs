using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Ceriyo.Data.GameObjects;

namespace Ceriyo.Data.ViewModels
{
    public class ObjectSelectionVM : BaseVM
    {
        private BindingList<Creature> _creatures;
        private BindingList<Item> _items;
        private BindingList<Placeable> _placeables;

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

        public ObjectSelectionVM()
        {
            this.Creatures = new BindingList<Creature>();
            this.Items = new BindingList<Item>();
            this.Placeables = new BindingList<Placeable>();
        }
    }
}
