using System;
using System.Collections.Generic;
using System.ComponentModel;
using Ceriyo.Core.Components;
using Ceriyo.Core.Entities;
using Prism.Commands;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;

namespace Ceriyo.Toolset.WPF.Views.ModulePropertiesView
{
    public class ModulePropertiesViewModel : BindableBase, IInteractionRequestAware
    {
        public ModulePropertiesViewModel()
        {
            Tag = new Tag();
            Resref = new Resref();
            Scripts = new BindingList<Script>();
            MaximumPossibleLevel = 99;

            LocalNumbers = new Dictionary<string, float>();
            LocalStrings = new Dictionary<string, string>();

            SaveCommand = new DelegateCommand(Save);
            CancelCommand = new DelegateCommand(Cancel);
        }

        private string _name;

        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        private Tag _tag;

        public Tag Tag
        {
            get { return _tag; }
            set { SetProperty(ref _tag, value); }
        }

        private Resref _resref;

        public Resref Resref
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

        private Dictionary<string, string> _localStrings;

        public Dictionary<string, string> LocalStrings
        {
            get { return _localStrings; }
            set { SetProperty(ref _localStrings, value); }
        }

        private Dictionary<string, float> _localNumbers;
        public Dictionary<string, float> LocalNumbers
        {
            get { return _localNumbers; }
            set { SetProperty(ref _localNumbers, value); }
        }


        private BindingList<Script> _scripts;

        public BindingList<Script> Scripts
        {
            get { return _scripts; }
            set { SetProperty(ref _scripts, value); }
        }

        private Script _onPlayerEnter;

        public Script OnPlayerEnter
        {
            get { return _onPlayerEnter; }
            set { SetProperty(ref _onPlayerEnter, value); }
        }

        private Script _onPlayerLeaving;

        public Script OnPlayerLeaving
        {
            get { return _onPlayerLeaving; }
            set { SetProperty(ref _onPlayerLeaving, value); }
        }

        private Script _onPlayerLeft;

        public Script OnPlayerLeft
        {
            get { return _onPlayerLeft; }
            set { SetProperty(ref _onPlayerLeft, value); }
        }

        private Script _onHeartbeat;

        public Script OnHeartbeat
        {
            get { return _onHeartbeat; }
            set { SetProperty(ref _onHeartbeat, value); }
        }

        private Script _onModuleLoad;

        public Script OnModuleLoad
        {
            get { return _onModuleLoad; }
            set { SetProperty(ref _onModuleLoad, value); }
        }

        private Script _onPlayerDying;

        public Script OnPlayerDying
        {
            get { return _onPlayerDying; }
            set { SetProperty(ref _onPlayerDying, value); }
        }

        private Script _onPlayerDeath;

        public Script OnPlayerDeath
        {
            get { return _onPlayerDeath; }
            set { SetProperty(ref _onPlayerDeath, value); }
        }

        private Script _onPlayerRespawn;

        public Script OnPlayerRespawn
        {
            get { return _onPlayerRespawn; }
            set { SetProperty(ref _onPlayerRespawn, value); }
        }

        private Script _onPlayerLevelUp;

        public Script OnPlayerLevelUp
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

            FinishInteraction();
        }

        public DelegateCommand CancelCommand { get; set; }

        private void Cancel()
        {

            FinishInteraction();
        }

        public INotification Notification { get; set; }
        public Action FinishInteraction { get; set; }
    }
}
