using System;
using System.ComponentModel;
using Ceriyo.Core.Components;
using Ceriyo.Core.Data;
using Ceriyo.Core.Entities;
using Ceriyo.Toolset.WPF.Events.Module;
using Prism.Commands;
using Prism.Events;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;

namespace Ceriyo.Toolset.WPF.Views.ModulePropertiesView
{
    public class ModulePropertiesViewModel : BindableBase, IInteractionRequestAware
    {
        private readonly IEventAggregator _eventAggregator;

        public ModulePropertiesViewModel()
        {
            
        }

        public ModulePropertiesViewModel(ModuleData moduleData,
            IEventAggregator eventAggregator)
        {
            Scripts = new BindingList<Script>();
            MaximumPossibleLevel = 99;



            SaveCommand = new DelegateCommand(Save);
            CancelCommand = new DelegateCommand(Cancel);
            _moduleData = moduleData;

            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<ModuleLoadedEvent>().Subscribe(ModuleLoaded);
            _eventAggregator.GetEvent<ModulePropertiesClosedEvent>().Subscribe(Cancel);
        }


        private ModuleData _moduleData;

        public ModuleData ModuleData
        {
            get { return _moduleData;}
            set { SetProperty(ref _moduleData, value); }
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
            set { SetProperty(ref _maxLevel, value); }
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

        private LocalVariableData _localVariables;

        public LocalVariableData LocalVariables
        {
            get { return _localVariables; }
            set { SetProperty(ref _localVariables, value); }
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
            ModuleData.Name = Name;
            ModuleData.Tag = Tag;
            ModuleData.Resref = Resref;
            ModuleData.Description = Description;
            ModuleData.Comments = Comments;
            ModuleData.MaxLevel = MaxLevel;

            ModuleData.OnPlayerEnter = OnPlayerEnter;
            ModuleData.OnPlayerLeaving = OnPlayerLeaving;
            ModuleData.OnPlayerLeft = OnPlayerLeft;
            ModuleData.OnHeartbeat = OnHeartbeat;
            ModuleData.OnModuleLoad = OnModuleLoad;
            ModuleData.OnPlayerDying = OnPlayerDying;
            ModuleData.OnPlayerDeath = OnPlayerDeath;
            ModuleData.OnPlayerRespawn = OnPlayerRespawn;
            ModuleData.OnPlayerLevelUp = OnPlayerLevelUp;

            ModuleData.LocalVariables = LocalVariables;
            ModuleData.LevelChart = LevelChart;

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
            Name = ModuleData.Name;
            Tag = ModuleData.Tag;
            Resref = ModuleData.Resref;
            Description = ModuleData.Description;
            Comments = ModuleData.Comments;
            MaxLevel = ModuleData.MaxLevel;

            OnPlayerEnter = ModuleData.OnPlayerEnter;
            OnPlayerLeaving = ModuleData.OnPlayerLeaving;
            OnPlayerLeft = ModuleData.OnPlayerLeft;
            OnHeartbeat = ModuleData.OnHeartbeat;
            OnModuleLoad = ModuleData.OnModuleLoad;
            OnPlayerDying = ModuleData.OnPlayerDying;
            OnPlayerDeath = ModuleData.OnPlayerDeath;
            OnPlayerRespawn = ModuleData.OnPlayerRespawn;
            OnPlayerLevelUp = ModuleData.OnPlayerLevelUp;

            LocalVariables = ModuleData.LocalVariables;
            LevelChart = ModuleData.LevelChart;
        }

        private void ModuleLoaded(string moduleFileName)
        {
            CopyPropertiesFromModuleData();
        }

        public INotification Notification { get; set; }
        public Action FinishInteraction { get; set; }
    }
}
