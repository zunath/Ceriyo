using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Ceriyo.Data.GameObjects;

namespace Ceriyo.Data.ViewModels
{
    public class ModulePropertiesVM : BaseVM
    {
        private string _name;
        private string _tag;
        private string _resref;
        private BindingList<string> _scripts;
        private BindingList<LocalVariable> _localVariables;
        private string _description;
        private string _comments;
        private int _maxLevel;
        private LevelChart _levels;
        private string _onPlayerEnterScript;
        private string _onPlayerLeavingScript;
        private string _onPlayerLeftScript;
        private string _onHeartbeatScript;
        private string _onModuleLoadScript;
        private string _onPlayerDyingScript;
        private string _onPlayerDeathScript;
        private string _onPlayerRespawnScript;

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

        public int MaxLevel
        {
            get
            {
                return _maxLevel;
            }
            set
            {
                _maxLevel = value;
                OnPropertyChanged("MaxLevel");
            }
        }

        public LevelChart Levels
        {
            get
            {
                return _levels;
            }
            set
            {
                _levels = value;
                OnPropertyChanged("Levels");
            }
        }

        public string OnPlayerEnterScript
        {
            get
            {
                return _onPlayerEnterScript;
            }
            set
            {
                _onPlayerEnterScript = value;
                OnPropertyChanged("OnPlayerEnterScript");
            }
        }

        public string OnPlayerLeavingScript
        {
            get
            {
                return _onPlayerLeavingScript;
            }
            set
            {
                _onPlayerLeavingScript = value;
                OnPropertyChanged("OnPlayerLeavingScript");
            }
        }

        public string OnPlayerLeftScript
        {
            get
            {
                return _onPlayerLeftScript;
            }
            set
            {
                _onPlayerLeftScript = value;
                OnPropertyChanged("OnPlayerLeftScript");
            }
        }

        public string OnHeartbeatScript
        {
            get
            {
                return _onHeartbeatScript;
            }
            set
            {
                _onHeartbeatScript = value;
                OnPropertyChanged("OnHeartbeatScript");
            }
        }

        public string OnModuleLoadScript
        {
            get
            {
                return _onModuleLoadScript;
            }
            set
            {
                _onModuleLoadScript = value;
                OnPropertyChanged("OnModuleLoadScript");
            }
        }

        public string OnPlayerDyingScript
        {
            get
            {
                return _onPlayerDyingScript;
            }
            set
            {
                _onPlayerDyingScript = value;
                OnPropertyChanged("OnPlayerDyingScript");
            }
        }

        public string OnPlayerDeathScript
        {
            get
            {
                return _onPlayerDeathScript;
            }
            set
            {
                _onPlayerDeathScript = value;
                OnPropertyChanged("OnPlayerDeathScript");
            }
        }

        public string OnPlayerRespawnScript
        {
            get
            {
                return _onPlayerRespawnScript;
            }
            set
            {
                _onPlayerRespawnScript = value;
                OnPropertyChanged("OnPlayerRespawnScript");
            }
        }

        public ModulePropertiesVM()
        {
            this.Comments = "";
            this.Description = "";
            this.LocalVariables = new BindingList<LocalVariable>();
            this.Name = "";
            this.Resref = "";
            this.Scripts = new BindingList<string>();
            this.Tag = "";
            this.Levels = new LevelChart();
            this.MaxLevel = EngineConstants.MaxLevel;

            this.OnPlayerEnterScript = "";
            this.OnPlayerLeavingScript = "";
            this.OnPlayerLeftScript = "";
            this.OnHeartbeatScript = "";
            this.OnModuleLoadScript = "";
            this.OnPlayerDyingScript = "";
            this.OnPlayerDeathScript = "";
            this.OnPlayerRespawnScript = "";
        }
    }
}
