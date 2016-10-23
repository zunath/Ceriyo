using System;
using System.Collections.Specialized;
using System.ComponentModel;
using Ceriyo.Core.Contracts;
using Ceriyo.Core.Data;
using Ceriyo.Core.Observables;
using Ceriyo.Core.Services.Contracts;
using Ceriyo.Infrastructure.WPF.BindableBases;
using Ceriyo.Infrastructure.WPF.Factory.Contracts;
using Ceriyo.Infrastructure.WPF.Observables;
using Ceriyo.Toolset.WPF.Events.Module;
using Prism.Commands;
using Prism.Events;
using Prism.Interactivity.InteractionRequest;

namespace Ceriyo.Toolset.WPF.Views.ModulePropertiesView
{
    public class ModulePropertiesViewModel : ValidatableBindableBase<ModulePropertiesViewModelValidator>, IInteractionRequestAware
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IModuleService _domainService;
        private readonly IObservableDataFactory _observableDataFactory;
        private readonly IObjectMapper _objectMapper;

        public ModulePropertiesViewModel(
            IObjectMapper objectMapper,
            IEventAggregator eventAggregator,
            IModuleService domainService,
            IObservableDataFactory observableDataFactory)
        {
            _objectMapper = objectMapper;
            _eventAggregator = eventAggregator;
            _domainService = domainService;
            _observableDataFactory = observableDataFactory;

            Scripts = new ObservableCollectionEx<ScriptDataObservable>();
            LocalVariables = _observableDataFactory.Create<LocalVariableDataObservable>();
            LevelChart = new ObservableCollectionEx<ClassLevelDataObservable>();

            
            LocalVariables.VariablesItemPropertyChanged += OnPropertyChanged;
            LocalVariables.VariablesPropertyChanged += OnPropertyChanged;
            LocalVariables.VariablesCollectionChanged += LocalVariablesCollectionChanged;
            LevelChart.PropertyChanged += OnPropertyChanged;
            LevelChart.ItemPropertyChanged += OnPropertyChanged;

            MaximumPossibleLevel = 99;
            
            SaveCommand = new DelegateCommand(Save, CanSave);
            CancelCommand = new DelegateCommand(Cancel);

            AddLocalStringCommand = new DelegateCommand(AddLocalString);
            AddLocalDoubleCommand = new DelegateCommand(AddLocalDouble);
            DeleteLocalStringCommand = new DelegateCommand<LocalStringDataObservable>(DeleteLocalString);
            DeleteLocalDoubleCommand = new DelegateCommand<LocalDoubleDataObservable>(DeleteLocalDouble);

            _eventAggregator.GetEvent<ModuleLoadedEvent>().Subscribe(ModuleLoaded);
            _eventAggregator.GetEvent<ModulePropertiesClosedEvent>().Subscribe(Cancel);

            PropertyChanged += OnPropertyChanged;
            LocalVariables.PropertyChanged += OnPropertyChanged;
        }

        private void LocalVariablesCollectionChanged(object sender, NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs)
        {
            SaveCommand.RaiseCanExecuteChanged();
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            SaveCommand.RaiseCanExecuteChanged();
        }
        
        private bool CanSave()
        {
            return !HasErrors;
        }

        private string _name;

        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        private string _tag;

        public string Tag
        {
            get { return _tag; }
            set { SetProperty(ref _tag, value); }
        }

        private string _resref;

        public string Resref
        {
            get { return _resref; }
            set { SetProperty(ref _resref, value); }
        }

        private int _maximumPossibleLevel;

        public int MaximumPossibleLevel
        {
            get { return _maximumPossibleLevel; }
            set { SetProperty(ref _maximumPossibleLevel, value); }
        }

        private int _maxLevel;

        public int MaxLevel
        {
            get { return _maxLevel; }
            set
            {
                SetProperty(ref _maxLevel, value);
                SetProperty(ref _maxLevelString, Convert.ToString(value));
            }
        }

        private string _maxLevelString;

        public string MaxLevelString
        {
            get
            {
                return _maxLevelString;
            }
            set
            {
                int intVal;
                string val = value;
                if (string.IsNullOrWhiteSpace(val) ||
                    !int.TryParse(val, out intVal)) 
                    val = Convert.ToString(MaxLevel);

                SetProperty(ref _maxLevelString, val);
                SetProperty(ref _maxLevel, Convert.ToInt32(val));
            }
        }

        private string _description;

        public string Description
        {
            get { return _description; }
            set { SetProperty(ref _description, value); }
        }

        private string _comments;

        public string Comments
        {
            get { return _comments; }
            set { SetProperty(ref _comments, value); }
        }

        private LocalVariableDataObservable _localVariables;

        public LocalVariableDataObservable LocalVariables
        {
            get { return _localVariables; }
            set { SetProperty(ref _localVariables, value); }
        }
        
        private ObservableCollectionEx<ScriptDataObservable> _scripts;

        public ObservableCollectionEx<ScriptDataObservable> Scripts
        {
            get { return _scripts; }
            set { SetProperty(ref _scripts, value); }
        }

        private string _onPlayerEnter;

        public string OnPlayerEnter
        {
            get { return _onPlayerEnter; }
            set { SetProperty(ref _onPlayerEnter, value); }
        }

        private string _onPlayerLeaving;

        public string OnPlayerLeaving
        {
            get { return _onPlayerLeaving; }
            set { SetProperty(ref _onPlayerLeaving, value); }
        }

        private string _onPlayerLeft;

        public string OnPlayerLeft
        {
            get { return _onPlayerLeft; }
            set { SetProperty(ref _onPlayerLeft, value); }
        }

        private string _onHeartbeat;

        public string OnHeartbeat
        {
            get { return _onHeartbeat; }
            set { SetProperty(ref _onHeartbeat, value); }
        }

        private string _onModuleLoad;

        public string OnModuleLoad
        {
            get { return _onModuleLoad; }
            set { SetProperty(ref _onModuleLoad, value); }
        }

        private string _onPlayerDying;

        public string OnPlayerDying
        {
            get { return _onPlayerDying; }
            set { SetProperty(ref _onPlayerDying, value); }
        }

        private string _onPlayerDeath;

        public string OnPlayerDeath
        {
            get { return _onPlayerDeath; }
            set { SetProperty(ref _onPlayerDeath, value); }
        }

        private string _onPlayerRespawn;

        public string OnPlayerRespawn
        {
            get { return _onPlayerRespawn; }
            set { SetProperty(ref _onPlayerRespawn, value); }
        }

        private string _onPlayerLevelUp;

        public string OnPlayerLevelUp
        {
            get { return _onPlayerLevelUp; }
            set { SetProperty(ref _onPlayerLevelUp, value); }
        }

        private ObservableCollectionEx<ClassLevelDataObservable> _levelChart;

        public ObservableCollectionEx<ClassLevelDataObservable> LevelChart
        {
            get { return _levelChart; }
            set { SetProperty(ref _levelChart, value); }
        }


        public DelegateCommand SaveCommand { get; set; }

        private void Save()
        {
            if (HasErrors) return;

            ModuleData moduleData = _domainService.GetLoadedModuleData();
            moduleData.Name = Name;
            moduleData.Tag = Tag;
            moduleData.Resref = Resref;
            moduleData.Description = Description;
            moduleData.Comment = Comments;
            moduleData.MaxLevel = MaxLevel;

            moduleData.OnPlayerEnter = OnPlayerEnter;
            moduleData.OnPlayerLeaving = OnPlayerLeaving;
            moduleData.OnPlayerLeft = OnPlayerLeft;
            moduleData.OnHeartbeat = OnHeartbeat;
            moduleData.OnModuleLoad = OnModuleLoad;
            moduleData.OnPlayerDying = OnPlayerDying;
            moduleData.OnPlayerDeath = OnPlayerDeath;
            moduleData.OnPlayerRespawn = OnPlayerRespawn;
            moduleData.OnPlayerLevelUp = OnPlayerLevelUp;
            
            moduleData.LocalVariables.LocalStrings.Clear();
            foreach (var localString in LocalVariables.LocalStrings)
            {
                moduleData.LocalVariables.LocalStrings.Add(_objectMapper.Map<LocalStringData>(localString));
            }
            moduleData.LocalVariables.LocalDoubles.Clear();
            foreach (var localDouble in LocalVariables.LocalDoubles)
            {
                moduleData.LocalVariables.LocalDoubles.Add(_objectMapper.Map<LocalDoubleData>(localDouble));
            }

            moduleData.LevelChart.Clear();
            foreach (var level in LevelChart)
            {
                moduleData.LevelChart.Add(_objectMapper.Map<ClassLevelData>(level));
            }

            _domainService.UpdateLoadedModuleData(moduleData);

            FinishInteraction();

            _eventAggregator.GetEvent<ModulePropertiesChangedEvent>().Publish();
        }

        public DelegateCommand CancelCommand { get; set; }

        private void Cancel()
        {
            CopyPropertiesFromModuleData();
            FinishInteraction();
        }

        private void CopyPropertiesFromModuleData()
        {
            ModuleData moduleData = _domainService.GetLoadedModuleData();
            
            Name = moduleData.Name;
            Tag = moduleData.Tag;
            Resref = moduleData.Resref;
            Description = moduleData.Description;
            Comments = moduleData.Comment;
            MaxLevel = moduleData.MaxLevel;

            OnPlayerEnter = moduleData.OnPlayerEnter;
            OnPlayerLeaving = moduleData.OnPlayerLeaving;
            OnPlayerLeft = moduleData.OnPlayerLeft;
            OnHeartbeat = moduleData.OnHeartbeat;
            OnModuleLoad = moduleData.OnModuleLoad;
            OnPlayerDying = moduleData.OnPlayerDying;
            OnPlayerDeath = moduleData.OnPlayerDeath;
            OnPlayerRespawn = moduleData.OnPlayerRespawn;
            OnPlayerLevelUp = moduleData.OnPlayerLevelUp;
            
            LocalVariables.LocalStrings.Clear();
            foreach (var data in moduleData.LocalVariables.LocalStrings)
            {
                var localString = _observableDataFactory.CreateAndMap<LocalStringDataObservable, LocalStringData>(data);
                LocalVariables.LocalStrings.Add(localString);
            }

            LocalVariables.LocalDoubles.Clear();
            foreach (var data in moduleData.LocalVariables.LocalDoubles)
            {
                var localDouble = _observableDataFactory.CreateAndMap<LocalDoubleDataObservable, LocalDoubleData>(data);
                LocalVariables.LocalDoubles.Add(localDouble);
            }
            
            LevelChart.Clear();
            foreach (var level in moduleData.LevelChart)
            {
                LevelChart.Add(_observableDataFactory.CreateAndMap<ClassLevelDataObservable, ClassLevelData>(level));
            }

        }


        private void ModuleLoaded(string moduleFileName)
        {
            CopyPropertiesFromModuleData();
        }

        public DelegateCommand AddLocalStringCommand { get; }

        private void AddLocalString()
        {
            LocalVariables.LocalStrings.Add(_observableDataFactory.Create<LocalStringDataObservable>());
        }

        public DelegateCommand<LocalStringDataObservable> DeleteLocalStringCommand { get; }

        private void DeleteLocalString(LocalStringDataObservable localString)
        {
            LocalVariables.LocalStrings.Remove(localString);
        }

        public DelegateCommand AddLocalDoubleCommand { get; }

        private void AddLocalDouble()
        {
            LocalVariables.LocalDoubles.Add(_observableDataFactory.Create<LocalDoubleDataObservable>());
        }

        public DelegateCommand<LocalDoubleDataObservable> DeleteLocalDoubleCommand { get; }

        private void DeleteLocalDouble(LocalDoubleDataObservable localDouble)
        {
            LocalVariables.LocalDoubles.Remove(localDouble);
        }

        public INotification Notification { get; set; }
        public Action FinishInteraction { get; set; }
    }
}
