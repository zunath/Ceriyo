using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Ceriyo.Core.Components;
using Ceriyo.Core.Data;
using Ceriyo.Core.Entities;
using Ceriyo.Core.Observables;
using Ceriyo.Domain.Services.DataServices.Contracts;
using Ceriyo.Infrastructure.WPF.BindableBases;
using Ceriyo.Toolset.WPF.Events.Module;
using FluentValidation;
using Prism.Commands;
using Prism.Events;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;

namespace Ceriyo.Toolset.WPF.Views.ModulePropertiesView
{
    public class ModulePropertiesViewModel : ValidatableBindableBase, IInteractionRequestAware
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IModuleDomainService _domainService;

        public ModulePropertiesViewModel()
        {
            
        }

        public ModulePropertiesViewModel(IEventAggregator eventAggregator,
            IModuleDomainService domainService)
        {
            _eventAggregator = eventAggregator;
            _domainService = domainService;
            Scripts = new BindingList<Script>();
            LocalStrings = new ObservableCollectionEx<LocalStringData>();
            LocalFloats = new ObservableCollectionEx<LocalFloatData>();

            MaximumPossibleLevel = 99;
            
            SaveCommand = new DelegateCommand(Save, CanSave);
            CancelCommand = new DelegateCommand(Cancel);
            
            _eventAggregator.GetEvent<ModuleLoadedEvent>().Subscribe(ModuleLoaded);
            _eventAggregator.GetEvent<ModulePropertiesClosedEvent>().Subscribe(Cancel);

            PropertyChanged += OnPropertyChanged;
            LocalStrings.PropertyChanged += OnPropertyChanged;
            LocalStrings.ItemPropertyChanged += OnPropertyChanged;
            LocalFloats.PropertyChanged += OnPropertyChanged;
            LocalFloats.ItemPropertyChanged += OnPropertyChanged;
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            SaveCommand.RaiseCanExecuteChanged();
        }
        

        private bool CanSave()
        {
            var result = _validator.Validate(this);
            return result.IsValid;
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

        private ObservableCollectionEx<LocalStringData> _localStrings;

        public ObservableCollectionEx<LocalStringData> LocalStrings
        {
            get { return _localStrings; }
            set { SetProperty(ref _localStrings, value); }
        }

        private ObservableCollectionEx<LocalFloatData> _localFloats;

        public ObservableCollectionEx<LocalFloatData> LocalFloats
        {
            get { return _localFloats; }
            set { SetProperty(ref _localFloats, value); }
        }

        private BindingList<Script> _scripts;

        public BindingList<Script> Scripts
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

        private BindingList<ClassLevel> _levelChart;

        public BindingList<ClassLevel> LevelChart
        {
            get { return _levelChart; }
            set { SetProperty(ref _levelChart, value); }
        }


        public DelegateCommand SaveCommand { get; set; }

        private void Save()
        {
            if (!_validator.Validate(this).IsValid) return;

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
            foreach (var record in LocalStrings)
            {
                moduleData.LocalVariables.LocalStrings.Add(record.Key, record.Value);
            }
            
            moduleData.LocalVariables.LocalFloats.Clear();
            foreach (var record in LocalFloats)
            {
                moduleData.LocalVariables.LocalFloats.Add(record.Key, record.Value);
            }

            moduleData.LevelChart = LevelChart;

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
            
            LocalStrings.Clear();
            foreach (var record in moduleData.LocalVariables.LocalStrings)
            {
                LocalStrings.Add(new LocalStringData(record.Key, record.Value));
            }
            LocalStrings.Add(new LocalStringData("key1", "val1"));
            LocalStrings.Add(new LocalStringData("key2", "val2"));
            LocalStrings.Add(new LocalStringData("key3", "val3"));

            LocalFloats.Clear();
            foreach (var record in moduleData.LocalVariables.LocalFloats)
            {
                LocalFloats.Add(new LocalFloatData(record.Key, record.Value));
            }

            LevelChart = moduleData.LevelChart;
        }

        private void ModuleLoaded(string moduleFileName)
        {
            CopyPropertiesFromModuleData();
        }

        public INotification Notification { get; set; }
        public Action FinishInteraction { get; set; }

        private IValidator _validator;

        protected override IValidator Validator => _validator ?? (_validator = new ModulePropertiesViewModelValidator());
    }
}
