using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Ceriyo.Core.Data;
using Ceriyo.Core.Extensions;
using Ceriyo.Toolset.WPF.Events;
using Prism.Commands;
using Prism.Events;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;

namespace Ceriyo.Toolset.WPF.Views.AbilityEditorView
{
    public class AbilityEditorViewModel : BindableBase
    {
        private readonly IEventAggregator _eventAggregator;

        public AbilityEditorViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;

            NewCommand = new DelegateCommand(New);
            DeleteCommand = new DelegateCommand(Delete);

            Abilities = new ObservableCollectionEx<AbilityData>();
            Scripts = new Dictionary<string, ScriptData>();

            ConfirmDeleteRequest = new InteractionRequest<IConfirmation>();

            _eventAggregator.GetEvent<DataEditorClosedEvent>().Subscribe(DataEditorClosed);

            Abilities.ItemPropertyChanged += AbilitiesOnItemPropertyChanged;    
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
                    string globalID = SelectedAbility.GlobalID;
                    Abilities.Remove(SelectedAbility);
                    _eventAggregator.GetEvent<AbilityDeletedEvent>().Publish(globalID);
                });
        }

        private void DataEditorClosed(bool saveData)
        {
            if (saveData)
            {
                
            }
        }

        public InteractionRequest<IConfirmation> ConfirmDeleteRequest { get; }

    }
}
