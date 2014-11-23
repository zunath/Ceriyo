using System.ComponentModel;
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
        private Creature _selectedCreature;
        private Item _selectedItem;
        private Placeable _selectedPlaceable;
        private int _selectionStartX;
        private int _selectionStartY;
        private int _selectionEndX;
        private int _selectionEndY;
        private bool _isMouseDown;

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

        public Creature SelectedCreature
        {
            get
            {
                return _selectedCreature;
            }
            set
            {
                _selectedCreature = value;
                OnPropertyChanged("SelectedCreature");
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

        public Placeable SelectedPlaceable
        {
            get
            {
                return _selectedPlaceable;
            }
            set
            {
                _selectedPlaceable = value;
                OnPropertyChanged("SelectedPlaceable");
            }
        }

        public bool IsMouseDown
        {
            get
            {
                return _isMouseDown;
            }
            set
            {
                _isMouseDown = value;
                OnPropertyChanged("IsMouseDown");
            }
        }

        public PaintObjectsVM()
        {
            PaintMode = PaintObjectModeTypeEnum.None;
            Creatures = new BindingList<Creature>();
            Items = new BindingList<Item>();
            Placeables = new BindingList<Placeable>();
            Graphic = new GameResource();
            SelectedCreature = new Creature();
            SelectedItem = new Item();
            SelectedPlaceable = new Placeable();
            SelectionStartX = 0;
            SelectionStartY = 0;
            SelectionEndX = 0;
            SelectionEndY = 0;
            IsMouseDown = false;
        }
    }
}
