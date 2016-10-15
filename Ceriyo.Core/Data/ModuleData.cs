using System;
using System.ComponentModel;
using Ceriyo.Core.Entities;
using Ceriyo.Core.Validation;
using Ceriyo.Core.Validation.Data;
using FluentValidation;

namespace Ceriyo.Core.Data
{
    public class ModuleData: BaseValidatable
    {
        private string _name;
        private string _tag;
        private string _resref;
        private int _maxLevel;
        private string _description;
        private string _comment;

        private BindingList<string> _tilesetIDs;
        private BindingList<string> _skillIDs;
        private BindingList<string> _scriptIDs;
        private BindingList<string> _placeableIDs;
        private BindingList<string> _itemPropertyIDs;
        private BindingList<string> _itemIDs;
        private BindingList<string> _creatureIDs;
        private BindingList<string> _classIDs;
        private BindingList<string> _abilityIDs;

        private string _onPlayerEnter;
        private string _onPlayerLeaving;
        private string _onPlayerLeft;
        private string _onHeartbeat;
        private string _onModuleLoad;
        private string _onPlayerDying;
        private string _onPlayerDeath;
        private string _onPlayerRespawn;
        private string _onPlayerLevelUp;

        private LocalVariableData _localVariables;
        private BindingList<ClassLevel> _levelChart;
        private string _globalID;

        private BindingList<string> _resourcePacks;

        public BindingList<string> ResourcePacks
        {
            get { return _resourcePacks; }
            set
            {
                _resourcePacks = value;
                OnPropertyChanged();
            }
        }

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }
        public string Tag
        {
            get { return _tag; }
            set
            {
                _tag = value;
                OnPropertyChanged();
            }
        }
        public string Resref
        {
            get { return _resref; }
            set
            {
                _resref = value;
                OnPropertyChanged();
            }
        }

        public int MaxLevel
        {
            get { return _maxLevel; }
            set
            {
                _maxLevel = value;
                OnPropertyChanged();
            }
        }

        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                OnPropertyChanged();
            }
        }
        public string Comment
        {
            get { return _comment; }
            set
            {
                _comment = value;
                OnPropertyChanged();
            }
        }

        public BindingList<string> AbilityIDs
        {
            get { return _abilityIDs; }
            private set
            {
                if (Equals(value, _abilityIDs)) return;
                _abilityIDs = value;
                OnPropertyChanged();
            }
        }

        public BindingList<string> ClassIDs
        {
            get { return _classIDs; }
            private set
            {
                if (Equals(value, _classIDs)) return;
                _classIDs = value;
                OnPropertyChanged();
            }
        }

        public BindingList<string> CreatureIDs
        {
            get { return _creatureIDs; }
            private set
            {
                if (Equals(value, _creatureIDs)) return;
                _creatureIDs = value;
                OnPropertyChanged();
            }
        }

        public BindingList<string> ItemIDs
        {
            get { return _itemIDs; }
            private set
            {
                if (Equals(value, _itemIDs)) return;
                _itemIDs = value;
                OnPropertyChanged();
            }
        }

        public BindingList<string> ItemPropertyIDs
        {
            get { return _itemPropertyIDs; }
            private set
            {
                if (Equals(value, _itemPropertyIDs)) return;
                _itemPropertyIDs = value;
                OnPropertyChanged();
            }
        }

        public BindingList<string> PlaceableIDs
        {
            get { return _placeableIDs; }
            private set
            {
                if (Equals(value, _placeableIDs)) return;
                _placeableIDs = value;
                OnPropertyChanged();
            }
        }

        public BindingList<string> ScriptIDs
        {
            get { return _scriptIDs; }
            private set
            {
                if (Equals(value, _scriptIDs)) return;
                _scriptIDs = value;
                OnPropertyChanged();
            }
        }

        public BindingList<string> SkillIDs
        {
            get { return _skillIDs; }
            private set
            {
                if (Equals(value, _skillIDs)) return;
                _skillIDs = value;
                OnPropertyChanged();
            }
        }

        public BindingList<string> TilesetIDs
        {
            get { return _tilesetIDs; }
            private set
            {
                if (Equals(value, _tilesetIDs)) return;
                _tilesetIDs = value;
                OnPropertyChanged();
            }
        }

        public string OnPlayerLevelUp
        {
            get { return _onPlayerLevelUp; }
            set
            {
                _onPlayerLevelUp = value;
                OnPropertyChanged();
            }
        }

        public string OnPlayerEnter
        {
            get { return _onPlayerEnter; }
            set
            {
                _onPlayerEnter = value;
                OnPropertyChanged();
            }
        }

        public string OnPlayerLeaving
        {
            get { return _onPlayerLeaving; }
            set
            {
                _onPlayerLeaving = value;
                OnPropertyChanged();
            }
        }

        public string OnPlayerLeft
        {
            get { return _onPlayerLeft; }
            set
            {
                _onPlayerLeft = value;
                OnPropertyChanged();
            }
        }

        public string OnHeartbeat
        {
            get { return _onHeartbeat; }
            set
            {
                _onHeartbeat = value;
                OnPropertyChanged();
            }
        }

        public string OnModuleLoad
        {
            get { return _onModuleLoad; }
            set
            {
                _onModuleLoad = value;
                OnPropertyChanged();
            }
        }

        public string OnPlayerDying
        {
            get { return _onPlayerDying; }
            set
            {
                _onPlayerDying = value;
                OnPropertyChanged();
            }
        }

        public string OnPlayerDeath
        {
            get { return _onPlayerDeath; }
            set
            {
                _onPlayerDeath = value;
                OnPropertyChanged();
            }
        }

        public string OnPlayerRespawn
        {
            get { return _onPlayerRespawn; }
            set
            {
                _onPlayerRespawn = value;
                OnPropertyChanged();
            }
        }

        public LocalVariableData LocalVariables
        {
            get { return _localVariables; }
            set
            {
                _localVariables = value;
                OnPropertyChanged();
            }
        }

        public BindingList<ClassLevel> LevelChart
        {
            get { return _levelChart; }
            set
            {
                _levelChart = value;
                OnPropertyChanged();
            }
        }

        public string GlobalID
        {
            get { return _globalID; }
            set
            {
                _globalID = value;
                OnPropertyChanged();
            }
        }

        public ModuleData()
        {
            GlobalID = Guid.NewGuid().ToString();

            AbilityIDs = new BindingList<string>();
            ClassIDs = new BindingList<string>();
            CreatureIDs = new BindingList<string>();
            ItemIDs = new BindingList<string>();
            ItemPropertyIDs = new BindingList<string>();
            PlaceableIDs = new BindingList<string>();
            ScriptIDs = new BindingList<string>();
            SkillIDs = new BindingList<string>();
            TilesetIDs = new BindingList<string>();
            MaxLevel = 99;
            LocalVariables = new LocalVariableData();
            LevelChart = new BindingList<ClassLevel>();
            ResourcePacks = new BindingList<string>();
        }


        private IValidator _validator;
        protected override IValidator Validator => _validator ?? (_validator = new ModuleDataValidator());
    }
}
