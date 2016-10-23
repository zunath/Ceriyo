using System.Collections.Generic;
using System.ComponentModel;
using Ceriyo.Core.Data;
using Ceriyo.Core.Observables;
using Ceriyo.Core.Services.Contracts;
using Ceriyo.Infrastructure.WPF.BindableBases;
using Ceriyo.Infrastructure.WPF.Factory.Contracts;
using Ceriyo.Infrastructure.WPF.Observables;
using Ceriyo.Toolset.WPF.Events.Ability;
using Ceriyo.Toolset.WPF.Events.DataEditor;
using Ceriyo.Toolset.WPF.Events.Module;
using Prism.Commands;
using Prism.Events;
using Prism.Interactivity.InteractionRequest;

namespace Ceriyo.Toolset.WPF.Views.AbilityEditorView
{
    public class AbilityEditorViewModel : ValidatableBindableBase<AbilityEditorViewModelValidator>
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IObservableDataFactory _observableDataFactory;
        private readonly IModuleDataService _moduleDataService;

        public AbilityEditorViewModel(
            IEventAggregator eventAggregator,
            IObservableDataFactory observableDataFactory,
            IModuleDataService moduleDataService)
        {
            _eventAggregator = eventAggregator;
            _observableDataFactory = observableDataFactory;
            _moduleDataService = moduleDataService;

            NewCommand = new DelegateCommand(New);
            DeleteCommand = new DelegateCommand(Delete);

            Abilities = new ObservableCollectionEx<AbilityDataObservable>();
            Scripts = new Dictionary<string, ScriptDataObservable>();

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
            foreach (var loaded in _moduleDataService.LoadAll<AbilityData>())
            {
                var ability = _observableDataFactory.CreateAndMap<AbilityDataObservable, AbilityData>(loaded);
                Abilities.Add(ability);
            }
        }

        private void RaiseValidityChangedEvent()
        {
            ValidateObject();
            _eventAggregator.GetEvent<AbilityEditorValidityChangedEvent>().Publish(!HasErrors);
        }

        private void AbilitiesOnItemPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            AbilityDataObservable ability = (AbilityDataObservable) sender;
            _eventAggregator.GetEvent<AbilityChangedEvent>().Publish(ability);
            RaiseValidityChangedEvent();
        }
        
        private ObservableCollectionEx<AbilityDataObservable> _abilities;

        public ObservableCollectionEx<AbilityDataObservable> Abilities
        {
            get { return _abilities; }
            set { SetProperty(ref _abilities, value); }
        }

        private AbilityDataObservable _selectedAbility;
        public AbilityDataObservable SelectedAbility
        {
            get { return _selectedAbility; }
            set
            {
                SetProperty(ref _selectedAbility, value);
                OnPropertyChanged(nameof(IsAbilitySelected));
            }
        }

        private Dictionary<string, ScriptDataObservable> _scripts;

        public Dictionary<string, ScriptDataObservable> Scripts
        {
            get { return _scripts; }
            set { SetProperty(ref _scripts, value); }
        }


        public bool IsAbilitySelected => SelectedAbility != null;


        public DelegateCommand NewCommand { get; set; }
        public DelegateCommand DeleteCommand { get; set; }

        private void New()
        {
            AbilityDataObservable ability = _observableDataFactory.Create<AbilityDataObservable>();
            ability.Name = "Ability" + (Abilities.Count + 1);
            Abilities.Add(ability);
            
            _eventAggregator.GetEvent<AbilityCreatedEvent>().Publish(ability);
            RaiseValidityChangedEvent();
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
                    RaiseValidityChangedEvent();
                });
        }
        

        public InteractionRequest<IConfirmation> ConfirmDeleteRequest { get; }
    }
}
