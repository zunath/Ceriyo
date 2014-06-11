using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Ceriyo.Data.GameObjects;
using Ceriyo.Data.ResourceObjects;

namespace Ceriyo.Data.ViewModels
{
    public class PlaceableEditorVM : BaseVM
    {
        private BindingList<GameResource> _graphics;
        private BindingList<Placeable> _placeables;
        private GameResource _graphic;
        private Placeable _selectedPlaceable;
        private bool _isPlaceableSelected;
        private BindingList<string> _scripts; 

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

        public bool IsPlaceableSelected
        {
            get
            {
                return _isPlaceableSelected;
            }
            set
            {
                _isPlaceableSelected = value;
                OnPropertyChanged("IsPlaceableSelected");
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

        public PlaceableEditorVM()
        {
            this.Graphics = new BindingList<GameResource>();
            this.IsPlaceableSelected = false;
            this.Placeables = new BindingList<Placeable>();
            this.Scripts = new BindingList<string>();
        }

    }
}
