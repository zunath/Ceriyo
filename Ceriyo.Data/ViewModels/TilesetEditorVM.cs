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
    public class TilesetEditorVM : BaseVM
    {
        private BindingList<LocalVariable> _localVariables;
        private BindingList<GameResource> _graphics;
        private BindingList<Tileset> _tilesets;
        private Tileset _selectedTileset;
        private bool _isTilesetSelected;
        private TileEditModeEnum _tileEditMode;

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
        
        public Tileset SelectedTileset
        {
            get
            {
                return _selectedTileset;
            }
            set
            {
                _selectedTileset = value;
                OnPropertyChanged("SelectedTileset");
            }
        }

        public BindingList<Tileset> Tilesets
        {
            get
            {
                return _tilesets;
            }
            set
            {
                _tilesets = value;
                OnPropertyChanged("Tilesets");
            }
        }

        public bool IsTilesetSelected
        {
            get
            {
                return _isTilesetSelected;
            }
            set
            {
                _isTilesetSelected = value;
                OnPropertyChanged("IsTilesetSelected");
            }
        }

        public TileEditModeEnum TileEditMode
        {
            get
            {
                return _tileEditMode;
            }
            set
            {
                _tileEditMode = value;
                OnPropertyChanged("TileEditMode");
            }
        }

        public TilesetEditorVM()
        {
            this.LocalVariables = new BindingList<LocalVariable>();
            this.Graphics = new BindingList<GameResource>();
            this.TileEditMode = TileEditModeEnum.Passability;
        }
    }
}
