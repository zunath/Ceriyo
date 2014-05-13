using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Ceriyo.Data.GameObjects;
using Ceriyo.Data.ResourceObjects;

namespace Ceriyo.Data.ViewModels
{
    public class TilesetEditorVM : BaseVM
    {
        private string _name;
        private string _tag;
        private string _resref;
        private string _description;
        private string _comments;
        private BindingList<LocalVariable> _localVariables;
        private BindingList<GameResource> _graphics;
        private BindingList<Tileset> _tilesets;
        private Tileset _selectedTileset;


        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }
        public string Tag
        {
            get
            {
                return _tag;
            }
            set
            {
                _tag = value;
                OnPropertyChanged("Tag");
            }
        }

        public string Resref
        {
            get
            {
                return _resref;
            }
            set
            {
                _resref = value;
                OnPropertyChanged("Resref");
            }
        }

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
        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                _description = value;
                OnPropertyChanged("Description");
            }
        }

        public string Comments
        {
            get
            {
                return _comments;
            }
            set
            {
                _comments = value;
                OnPropertyChanged("Comments");
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

        public TilesetEditorVM()
        {
            this.Name = "";
            this.Tag = "";
            this.Resref = "";
            this.LocalVariables = new BindingList<LocalVariable>();
            this.Description = "";
            this.Comments = "";
            this.Graphics = new BindingList<GameResource>();
            this.SelectedTileset = new Tileset();
        }
    }
}
