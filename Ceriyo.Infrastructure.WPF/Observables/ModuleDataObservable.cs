using System;
using System.ComponentModel;
using Ceriyo.Core.Contracts;
using Ceriyo.Core.Data;
using Ceriyo.Core.Observables;
using Ceriyo.Infrastructure.WPF.BindableBases;
using Ceriyo.Infrastructure.WPF.Validation.Validators;

namespace Ceriyo.Infrastructure.WPF.Observables
{
    public class ModuleDataObservable: ValidatableBindableBase<ModuleData>
    {
        public delegate ModuleDataObservable Factory();

        private string _name;
        private string _tag;
        private string _resref;
        private int _maxLevel;
        private string _description;
        private string _comment;

        private ObservableCollectionEx<string> _tilesetIDs;
        private ObservableCollectionEx<string> _skillIDs;
        private ObservableCollectionEx<string> _scriptIDs;
        private ObservableCollectionEx<string> _placeableIDs;
        private ObservableCollectionEx<string> _itemPropertyIDs;
        private ObservableCollectionEx<string> _itemIDs;
        private ObservableCollectionEx<string> _creatureIDs;
        private ObservableCollectionEx<string> _classIDs;
        private ObservableCollectionEx<string> _abilityIDs;

        private string _onPlayerEnter;
        private string _onPlayerLeaving;
        private string _onPlayerLeft;
        private string _onHeartbeat;
        private string _onModuleLoad;
        private string _onPlayerDying;
        private string _onPlayerDeath;
        private string _onPlayerRespawn;
        private string _onPlayerLevelUp;

        private LocalVariableDataObservable _localVariables;
        private LevelChartDataObservable _levelChart;
        private string _globalID;

        private BindingList<string> _resourcePacks;

        public BindingList<string> ResourcePacks
        {
            get { return _resourcePacks; }
            set { SetProperty(ref _resourcePacks, value); }
        }

        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }
        public string Tag
        {
            get { return _tag; }
            set { SetProperty(ref _tag, value); }
        }
        public string Resref
        {
            get { return _resref; }
            set { SetProperty(ref _resref, value); }
        }

        public int MaxLevel
        {
            get { return _maxLevel; }
            set { SetProperty(ref _maxLevel, value); }
        }

        public string Description
        {
            get { return _description; }
            set { SetProperty(ref _description, value); }
        }
        public string Comment
        {
            get { return _comment; }
            set { SetProperty(ref _comment, value); }
        }

        public ObservableCollectionEx<string> AbilityIDs
        {
            get { return _abilityIDs; }
            private set { SetProperty(ref _abilityIDs, value); }
        }

        public ObservableCollectionEx<string> ClassIDs
        {
            get { return _classIDs; }
            private set { SetProperty(ref _classIDs, value); }
        }

        public ObservableCollectionEx<string> CreatureIDs
        {
            get { return _creatureIDs; }
            private set { SetProperty(ref _creatureIDs, value); }
        }

        public ObservableCollectionEx<string> ItemIDs
        {
            get { return _itemIDs; }
            private set { SetProperty(ref _itemIDs, value); }
        }

        public ObservableCollectionEx<string> ItemPropertyIDs
        {
            get { return _itemPropertyIDs; }
            private set { SetProperty(ref _itemPropertyIDs, value); }
        }

        public ObservableCollectionEx<string> PlaceableIDs
        {
            get { return _placeableIDs; }
            private set { SetProperty(ref _placeableIDs, value); }
        }

        public ObservableCollectionEx<string> ScriptIDs
        {
            get { return _scriptIDs; }
            private set { SetProperty(ref _scriptIDs, value); }
        }

        public ObservableCollectionEx<string> SkillIDs
        {
            get { return _skillIDs; }
            private set { SetProperty(ref _skillIDs, value); }
        }

        public ObservableCollectionEx<string> TilesetIDs
        {
            get { return _tilesetIDs; }
            private set { SetProperty(ref _tilesetIDs, value); }
        }

        public string OnPlayerLevelUp
        {
            get { return _onPlayerLevelUp; }
            set { SetProperty(ref _onPlayerLevelUp, value); }
        }

        public string OnPlayerEnter
        {
            get { return _onPlayerEnter; }
            set { SetProperty(ref _onPlayerEnter, value); }
        }

        public string OnPlayerLeaving
        {
            get { return _onPlayerLeaving; }
            set { SetProperty(ref _onPlayerLeaving, value); }
        }

        public string OnPlayerLeft
        {
            get { return _onPlayerLeft; }
            set { SetProperty(ref _onPlayerLeft, value); }
        }

        public string OnHeartbeat
        {
            get { return _onHeartbeat; }
            set { SetProperty(ref _onHeartbeat, value); }
        }

        public string OnModuleLoad
        {
            get { return _onModuleLoad; }
            set { SetProperty(ref _onModuleLoad, value); }
        }

        public string OnPlayerDying
        {
            get { return _onPlayerDying; }
            set { SetProperty(ref _onPlayerDying, value); }
        }

        public string OnPlayerDeath
        {
            get { return _onPlayerDeath; }
            set { SetProperty(ref _onPlayerDeath, value); }
        }

        public string OnPlayerRespawn
        {
            get { return _onPlayerRespawn; }
            set { SetProperty(ref _onPlayerRespawn, value); }
        }

        public LocalVariableDataObservable LocalVariables
        {
            get { return _localVariables; }
            set { SetProperty(ref _localVariables, value); }
        }

        public LevelChartDataObservable LevelChart
        {
            get { return _levelChart; }
            set { SetProperty(ref _levelChart, value); }
        }

        public string GlobalID
        {
            get { return _globalID; }
            set { SetProperty(ref _globalID, value); }
        }
        
        public ModuleDataObservable(ModuleDataObservableValidator validator,
            IObjectMapper objectMapper,
            LocalVariableDataObservable.Factory localVariableFactory)
            :base(objectMapper, validator)
        {
            GlobalID = Guid.NewGuid().ToString();
            _localVariables = localVariableFactory.Invoke();
            _levelChart = new LevelChartDataObservable();
            _abilityIDs = new ObservableCollectionEx<string>();
            _classIDs = new ObservableCollectionEx<string>();
            _creatureIDs = new ObservableCollectionEx<string>();
            _itemIDs = new ObservableCollectionEx<string>();
            _itemPropertyIDs = new ObservableCollectionEx<string>();
            _placeableIDs = new ObservableCollectionEx<string>();
            _scriptIDs = new ObservableCollectionEx<string>();
            _skillIDs = new ObservableCollectionEx<string>();
            _tilesetIDs = new ObservableCollectionEx<string>();
        }
    }
}
