using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using Ceriyo.Core.Contracts;
using Ceriyo.Core.Data;
using Ceriyo.Core.Observables;
using Ceriyo.Core.Services.Contracts;
using Ceriyo.Infrastructure.WPF.BindableBases;
using Ceriyo.Infrastructure.WPF.Observables;
using Ceriyo.Toolset.WPF.Events.Class;
using Ceriyo.Toolset.WPF.Events.Creature;
using Ceriyo.Toolset.WPF.Events.DataEditor;
using Ceriyo.Toolset.WPF.Events.Module;
using Prism.Commands;
using Prism.Events;
using Prism.Interactivity.InteractionRequest;

namespace Ceriyo.Toolset.WPF.Views.CreatureEditorView
{
    public class CreatureEditorViewModel : ValidatableBindableBase<CreatureEditorViewModel>
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IDataService _dataService;
        private readonly IObjectMapper _objectMapper;
        private readonly IPathService _pathService;
        private readonly CreatureDataObservable.Factory _creatureFactory;
        private readonly ClassDataObservable.Factory _classFactory;
        private readonly LocalStringDataObservable.Factory _localStringFactory;
        private readonly LocalDoubleDataObservable.Factory _localDoubleFactory;

        public CreatureEditorViewModel(
            CreatureEditorViewModelValidator validator,
            IEventAggregator eventAggregator,
            IDataService dataService,
            IObjectMapper objectMapper,
            IPathService pathService,
            CreatureDataObservable.Factory creatureFactory,
            ClassDataObservable.Factory classFactory,
            LocalStringDataObservable.Factory localStringFactory,
            LocalDoubleDataObservable.Factory localDoubleFactory)
            :base(objectMapper, validator)
        {
            _eventAggregator = eventAggregator;
            _dataService = dataService;
            _objectMapper = objectMapper;
            _pathService = pathService;
            _creatureFactory = creatureFactory;
            _classFactory = classFactory;
            _localStringFactory = localStringFactory;
            _localDoubleFactory = localDoubleFactory;

            NewCommand = new DelegateCommand(New);
            DeleteCommand = new DelegateCommand(Delete);

            AddLocalStringCommand = new DelegateCommand(AddLocalString);
            AddLocalDoubleCommand = new DelegateCommand(AddLocalDouble);
            DeleteLocalStringCommand = new DelegateCommand<LocalStringDataObservable>(DeleteLocalString);
            DeleteLocalDoubleCommand = new DelegateCommand<LocalDoubleDataObservable>(DeleteLocalDouble);

            Creatures = new ObservableCollectionEx<CreatureDataObservable>();
            Scripts = new Dictionary<string, ScriptDataObservable>();
            Classes = new ObservableCollectionEx<ClassDataObservable>();
            MaximumLevel = 50;
            Dialogs = new ObservableCollectionEx<DialogDataObservable>();

            ConfirmDeleteRequest = new InteractionRequest<IConfirmation>();
            
            Creatures.ItemPropertyChanged += CreaturesOnItemPropertyChanged;
            _eventAggregator.GetEvent<ClassCreatedEvent>().Subscribe(ClassCreated);
            //_eventAggregator.GetEvent<ClassChangedEvent>().Subscribe(ClassChanged);
            _eventAggregator.GetEvent<ClassDeletedEvent>().Subscribe(ClassDeleted);

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
            Creatures.Clear();
            Classes.Clear();
        }

        private void DataEditorClosed(bool doSave)
        {
            LoadExistingData();
        }

        private void LoadExistingData()
        {
            Creatures.Clear();
            Classes.Clear();

            string[] files = Directory.GetFiles($"{_pathService.ModulesTempDirectory}Creature/", "*.dat");
            foreach (var file in files)
            {
                CreatureData data = _dataService.Load<CreatureData>(file);
                CreatureDataObservable creature = _creatureFactory.Invoke(data);
                Creatures.Add(creature);
            }

            files = Directory.GetFiles($"{_pathService.ModulesTempDirectory}Class/", "*.dat");
            foreach (var file in files)
            {
                ClassData data = _dataService.Load<ClassData>(file);
                ClassDataObservable @class = _classFactory.Invoke(data);
                Classes.Add(@class);
            }
        }

        private void RaiseValidityChangedEvent()
        {
            _eventAggregator.GetEvent<CreatureEditorValidityChangedEvent>().Publish(!HasErrors);
        }
        
        private void CreaturesOnItemPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            CreatureDataObservable creatureChanged = (CreatureDataObservable)sender;
            _eventAggregator.GetEvent<CreatureChangedEvent>().Publish(creatureChanged);
            RaiseValidityChangedEvent();
        }

        private void ClassCreated(ClassDataObservable @class)
        {
            Classes.Add(@class);
        }
        
        private void ClassDeleted(ClassDataObservable @class)
        {
            var existingClass = Classes.Single(x => x.GlobalID == @class.GlobalID);
            Classes.Remove(existingClass);
        }

        private ObservableCollectionEx<CreatureDataObservable> _creatures;

        public ObservableCollectionEx<CreatureDataObservable> Creatures
        {
            get { return _creatures; }
            set { SetProperty(ref _creatures, value); }
        }

        private CreatureDataObservable _selectedCreature;
        public CreatureDataObservable SelectedCreature
        {
            get { return _selectedCreature; }
            set
            {
                SetProperty(ref _selectedCreature, value);
                OnPropertyChanged(nameof(IsCreatureSelected));
            }
        }

        private Dictionary<string, ScriptDataObservable> _scripts;

        public Dictionary<string, ScriptDataObservable> Scripts
        {
            get { return _scripts; }
            set { SetProperty(ref _scripts, value); }
        }

        private ObservableCollectionEx<DialogDataObservable> _dialogs;

        public ObservableCollectionEx<DialogDataObservable> Dialogs
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

        private ObservableCollectionEx<ClassDataObservable> _classes;

        public ObservableCollectionEx<ClassDataObservable> Classes
        {
            get { return _classes; }
            set { SetProperty(ref _classes, value); }
        }


        public bool IsCreatureSelected => SelectedCreature != null;


        public DelegateCommand NewCommand { get; set; }
        public DelegateCommand DeleteCommand { get; set; }

        private void New()
        {
            var creature = _creatureFactory.Invoke();
            creature.Name = "Creature" + (Creatures.Count + 1);
            Creatures.Add(creature);

            _eventAggregator.GetEvent<CreatureCreatedEvent>().Publish(creature);
            RaiseValidityChangedEvent();
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
                    _eventAggregator.GetEvent<CreatureDeletedEvent>().Publish(SelectedCreature);
                    Creatures.Remove(SelectedCreature);
                    RaiseValidityChangedEvent();
                });
        }

        public DelegateCommand AddLocalStringCommand { get; }

        private void AddLocalString()
        {
            SelectedCreature.LocalVariables.LocalStrings.Add(_localStringFactory.Invoke());
        }

        public DelegateCommand<LocalStringDataObservable> DeleteLocalStringCommand { get; }

        private void DeleteLocalString(LocalStringDataObservable localString)
        {
            SelectedCreature.LocalVariables.LocalStrings.Remove(localString);
        }

        public DelegateCommand AddLocalDoubleCommand { get; }

        private void AddLocalDouble()
        {
            SelectedCreature.LocalVariables.LocalDoubles.Add(_localDoubleFactory.Invoke());
        }

        public DelegateCommand<LocalDoubleDataObservable> DeleteLocalDoubleCommand { get; }

        private void DeleteLocalDouble(LocalDoubleDataObservable localDouble)
        {
            SelectedCreature.LocalVariables.LocalDoubles.Remove(localDouble);
        }
        public InteractionRequest<IConfirmation> ConfirmDeleteRequest { get; }
    }
}
