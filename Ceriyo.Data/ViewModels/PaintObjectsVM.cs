using System;
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
        private int _selectionStartX;
        private int _selectionStartY;
        private int _selectionEndX;
        private int _selectionEndY;

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

        public int SelectionStartX
        {
            get
            {
                return _selectionStartX;
            }
            set
            {
                _selectionStartX = value;
                OnPropertyChanged("SelectionStartX");
            }
        }

        public int SelectionStartY
        {
            get
            {
                return _selectionStartY;
            }
            set
            {
                _selectionStartY = value;
                OnPropertyChanged("SelectionStartY");
            }
        }

        public int SelectionEndX
        {
            get
            {
                return _selectionEndX;
            }
            set
            {
                _selectionEndX = value;
                OnPropertyChanged("SelectionEndX");
            }
        }

        public int SelectionEndY
        {
            get
            {
                return _selectionEndY;
            }
            set
            {
                _selectionEndY = value;
                OnPropertyChanged("SelectionEndY");
            }
        }

        public PaintObjectsVM()
        {
            this.PaintMode = PaintObjectModeTypeEnum.None;
            this.Creatures = new BindingList<Creature>();
            this.Items = new BindingList<Item>();
            this.Placeables = new BindingList<Placeable>();
            this.Graphic = new GameResource();
            this.SelectionStartX = 0;
            this.SelectionStartY = 0;
            this.SelectionEndX = 0;
            this.SelectionEndY = 0;
        }
    }
}
