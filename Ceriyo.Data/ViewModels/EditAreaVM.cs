using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Ceriyo.Data.Extensions;
using Ceriyo.Data.GameObjects;

namespace Ceriyo.Data.ViewModels
{
    public class EditAreaVM : BaseVM
    {
        private string _name;
        private string _tag;
        private string _resref;
        private int _width;
        private int _height;
        private BindingList<string> _scripts;
        private ObservableCollection<LocalVariable> _localVariables;
        private BindingList<Tileset> _tilesets;
        private string _description;
        private string _comments;
        private string _onAreaEnterScript;
        private string _onAreaExitScript;
        private string _onAreaHeartbeatScript;

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
        public int Width 
        {
            get
            {
                return _width;
            }
            set
            {
                _width = value;
                OnPropertyChanged("Width");
            }
        }

        public int Height 
        {
            get
            {
                return _height;
            }
            set
            {
                _height = value;
                OnPropertyChanged("Height");
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

        public ObservableCollection<LocalVariable> LocalVariables
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
        public string OnAreaEnterScript 
        {
            get
            {
                return _onAreaEnterScript;
            }
            set
            {
                _onAreaEnterScript = value;
                OnPropertyChanged("OnAreaEnterScript");
            }
        }
        public string OnAreaExitScript 
        {
            get
            {
                return _onAreaExitScript;
            }
            set
            {
                _onAreaExitScript = value;
                OnPropertyChanged("OnAreaExitScript");
            }
        }
        public string OnAreaHeartbeatScript 
        {
            get
            {
                return _onAreaHeartbeatScript;
            }
            set
            {
                _onAreaHeartbeatScript = value;
                OnPropertyChanged("OnAreaHeartbeatScript");
            }
        }
        

        public EditAreaVM()
        {
            this.Name = "";
            this.Tag = "";
            this.Resref = "";
            this.Width = EngineConstants.AreaMinWidth;
            this.Height = EngineConstants.AreaMinHeight;
            this.Scripts = new BindingList<string>();
            this.LocalVariables = new ObservableCollection<LocalVariable>();
            this.Tilesets = new BindingList<Tileset>();
            this.Description = "";
            this.Comments = "";
            this.OnAreaEnterScript = "";
            this.OnAreaExitScript = "";
            this.OnAreaHeartbeatScript = "";
        }
    }
}
