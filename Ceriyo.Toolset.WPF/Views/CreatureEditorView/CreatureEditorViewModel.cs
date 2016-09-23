using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Ceriyo.Core.Contracts;
using Ceriyo.Core.Data;
using Ceriyo.Core.Extensions;
using Ceriyo.Toolset.WPF.Events;
using Prism.Commands;
using Prism.Events;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;

namespace Ceriyo.Toolset.WPF.Views.CreatureEditorView
{
    public class CreatureEditorViewModel : BindableBase
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IObjectMapper _objectMapper;

        public CreatureEditorViewModel(IEventAggregator eventAggregator,
            IObjectMapper objectMapper)
        {
            _eventAggregator = eventAggregator;
            _objectMapper = objectMapper;

            NewCommand = new DelegateCommand(New);
            DeleteCommand = new DelegateCommand(Delete);

            Creatures = new ObservableCollectionEx<CreatureData>();
            Scripts = new Dictionary<string, ScriptData>();
            Classes = new BindingList<ClassData>();
            MaximumLevel = 50;
            Dialogs = new BindingList<DialogData>();

            ConfirmDeleteRequest = new InteractionRequest<IConfirmation>();

            _eventAggregator.GetEvent<DataEditorClosedEvent>().Subscribe(DataEditorClosed);

            Creatures.ItemPropertyChanged += CreaturesOnItemPropertyChanged;
            _eventAggregator.GetEvent<ClassCreatedEvent>().Subscribe(ClassCreated);
            _eventAggregator.GetEvent<ClassChangedEvent>().Subscribe(ClassChanged);
            _eventAggregator.GetEvent<ClassDeletedEvent>().Subscribe(ClassDeleted);
        }

        private void CreaturesOnItemPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            CreatureData creatureChanged = sender as CreatureData;
            _eventAggregator.GetEvent<CreatureChangedEvent>().Publish(creatureChanged);
        }

        private void ClassCreated(ClassData @class)
        {
            Classes.Add(@class);
        }

        private void ClassChanged(ClassData @class)
        {
            var existing = Classes.Single(x => x.GlobalID == @class.GlobalID);
            _objectMapper.Map(@class, existing);
        }

        private void ClassDeleted(string globalID)
        {
            var @class = Classes.Single(x => x.GlobalID == globalID);
            Classes.Remove(@class);
        }

        private ObservableCollectionEx<CreatureData> _creatures;

        public ObservableCollectionEx<CreatureData> Creatures
        {
            get { return _creatures; }
            set { SetProperty(ref _creatures, value); }
        }

        private CreatureData _selectedCreature;
        public CreatureData SelectedCreature
        {
            get { return _selectedCreature; }
            set
            {
                SetProperty(ref _selectedCreature, value);
                OnPropertyChanged("IsCreatureSelected");
            }
        }

        private Dictionary<string, ScriptData> _scripts;

        public Dictionary<string, ScriptData> Scripts
        {
            get { return _scripts; }
            set { SetProperty(ref _scripts, value); }
        }

        private BindingList<DialogData> _dialogs;

        public BindingList<DialogData> Dialogs
        {
            get { return _dialogs; }
            set { SetProperty(ref _dialogs, value); }
        }

        private int _maximumLevel;

        public int MaximumLevel
        {
            get { return _maximumLevel; }
            set { SetProperty(ref _maximumLevel, value); }
        }

        private BindingList<ClassData> _classes;

        public BindingList<ClassData> Classes
        {
            get { return _classes; }
            set { SetProperty(ref _classes, value); }
        }


        public bool IsCreatureSelected => SelectedCreature != null;


        public DelegateCommand NewCommand { get; set; }
        public DelegateCommand DeleteCommand { get; set; }

        private void New()
        {
            CreatureData creature = new CreatureData
            {
                Name = "Creature" + (Creatures.Count + 1)
            };
            Creatures.Add(creature);

            _eventAggregator.GetEvent<CreatureCreatedEvent>().Publish(creature);
        }

        private void Delete()
        {
            ConfirmDeleteRequest.Raise(
                new Confirmation
                {
                    Title = "Delete Creature?",
                    Content = "Are you sure you want to delete this creature?"
                }, c =>
                {
                    if (!c.Confirmed) return;
                    string globalID = SelectedCreature.GlobalID;
                    Creatures.Remove(SelectedCreature);
                    _eventAggregator.GetEvent<CreatureDeletedEvent>().Publish(globalID);
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
