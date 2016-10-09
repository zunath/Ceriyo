using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using Ceriyo.Core.Contracts;
using Ceriyo.Core.Data;
using Ceriyo.Core.Extensions;
using Ceriyo.Core.Services.Contracts;
using Ceriyo.Toolset.WPF.Events.Class;
using Ceriyo.Toolset.WPF.Events.Creature;
using Ceriyo.Toolset.WPF.Events.DataEditor;
using Ceriyo.Toolset.WPF.Events.Module;
using Prism.Commands;
using Prism.Events;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;

namespace Ceriyo.Toolset.WPF.Views.CreatureEditorView
{
    public class CreatureEditorViewModel : BindableBase
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IDataService _dataService;
        private readonly IObjectMapper _objectMapper;
        private readonly IPathService _pathService;

        public CreatureEditorViewModel(IEventAggregator eventAggregator,
            IDataService dataService,
            IObjectMapper objectMapper,
            IPathService pathService)
        {
            _eventAggregator = eventAggregator;
            _dataService = dataService;
            _objectMapper = objectMapper;
            _pathService = pathService;

            NewCommand = new DelegateCommand(New);
            DeleteCommand = new DelegateCommand(Delete);

            Creatures = new ObservableCollectionEx<CreatureData>();
            Scripts = new Dictionary<string, ScriptData>();
            Classes = new BindingList<ClassData>();
            MaximumLevel = 50;
            Dialogs = new BindingList<DialogData>();

            ConfirmDeleteRequest = new InteractionRequest<IConfirmation>();
            
            Creatures.ItemPropertyChanged += CreaturesOnItemPropertyChanged;
            _eventAggregator.GetEvent<ClassCreatedEvent>().Subscribe(ClassCreated);
            _eventAggregator.GetEvent<ClassChangedEvent>().Subscribe(ClassChanged);
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
                Creatures.Add(_dataService.Load<CreatureData>(file));
            }

            files = Directory.GetFiles($"{_pathService.ModulesTempDirectory}Class/", "*.dat");
            foreach (var file in files)
            {
                Classes.Add(_dataService.Load<ClassData>(file));
            }
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

        private void ClassDeleted(ClassData @class)
        {
            var existingClass = Classes.Single(x => x.GlobalID == @class.GlobalID);
            Classes.Remove(existingClass);
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
                    _eventAggregator.GetEvent<CreatureDeletedEvent>().Publish(SelectedCreature);
                    Creatures.Remove(SelectedCreature);
                });
        }
        
        public InteractionRequest<IConfirmation> ConfirmDeleteRequest { get; }

    }
}
