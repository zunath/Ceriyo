using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using Ceriyo.Core.Contracts;
using Ceriyo.Core.Data;
using Ceriyo.Core.Observables;
using Ceriyo.Core.Services.Contracts;
using Ceriyo.Toolset.WPF.Events.Ability;
using Ceriyo.Toolset.WPF.Events.DataEditor;
using Ceriyo.Toolset.WPF.Events.Module;
using Prism.Commands;
using Prism.Events;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;

namespace Ceriyo.Toolset.WPF.Views.AbilityEditorView
{
    public class AbilityEditorViewModel : BindableBase
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IDataService _dataService;
        private readonly IPathService _pathService;

        public AbilityEditorViewModel(IEventAggregator eventAggregator,
            IDataService dataService,
            IPathService pathService)
        {
            _eventAggregator = eventAggregator;
            _dataService = dataService;
            _pathService = pathService;

            NewCommand = new DelegateCommand(New);
            DeleteCommand = new DelegateCommand(Delete);

            Abilities = new ObservableCollectionEx<AbilityData>();
            Scripts = new Dictionary<string, ScriptData>();

            ConfirmDeleteRequest = new InteractionRequest<IConfirmation>();
            Abilities.ItemPropertyChanged += AbilitiesOnItemPropertyChanged;

            _eventAggregator.GetEvent<ModuleLoadedEvent>().Subscribe(ModuleLoaded);
            _eventAggregator.GetEvent<DataEditorClosedEvent>().Subscribe(DataEditorClosed);
            _eventAggregator.GetEvent<ModuleClosedEvent>().Subscribe(ModuleClosed);
        }

        private void ModuleLoaded(string moduleFileName)
        {
            LoadExistingData();
        }

        private void ModuleClosed()
        {
            Abilities.Clear();
        }

        private void DataEditorClosed(bool doSave)
        {
            LoadExistingData();
        }

        private void LoadExistingData()
        {
            Abilities.Clear();
            string[] files = Directory.GetFiles($"{_pathService.ModulesTempDirectory}Ability/", "*.dat");

            foreach (var file in files)
            {
                Abilities.Add(_dataService.Load<AbilityData>(file));
            }
        }

        private void AbilitiesOnItemPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            AbilityData abilityChanged = sender as AbilityData;
            _eventAggregator.GetEvent<AbilityChangedEvent>().Publish(abilityChanged);
        }
        
        private ObservableCollectionEx<AbilityData> _abilities;

        public ObservableCollectionEx<AbilityData> Abilities
        {
            get { return _abilities; }
            set { SetProperty(ref _abilities, value); }
        }

        private AbilityData _selectedAbility;
        public AbilityData SelectedAbility
        {
            get { return _selectedAbility; }
            set
            {
                SetProperty(ref _selectedAbility, value);
                OnPropertyChanged("IsAbilitySelected");
            }
        }

        private Dictionary<string, ScriptData> _scripts;

        public Dictionary<string, ScriptData> Scripts
        {
            get { return _scripts; }
            set { SetProperty(ref _scripts, value); }
        }


        public bool IsAbilitySelected => SelectedAbility != null;


        public DelegateCommand NewCommand { get; set; }
        public DelegateCommand DeleteCommand { get; set; }

        private void New()
        {
            AbilityData ability = new AbilityData
            {
                Name = "Ability" + (Abilities.Count+1)
            };
            Abilities.Add(ability);
            
            _eventAggregator.GetEvent<AbilityCreatedEvent>().Publish(ability);
        }

        private void Delete()
        {
            ConfirmDeleteRequest.Raise(
                new Confirmation
                {
                    Title = "Delete Ability?",
                    Content = "Are you sure you want to delete this ability?"
                }, c =>
                {
                    if (!c.Confirmed) return;
                    _eventAggregator.GetEvent<AbilityDeletedEvent>().Publish(SelectedAbility);
                    Abilities.Remove(SelectedAbility);
                });
        }
        

        public InteractionRequest<IConfirmation> ConfirmDeleteRequest { get; }

    }
}
