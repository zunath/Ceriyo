using System.ComponentModel;
using System.IO;
using Ceriyo.Core.Contracts;
using Ceriyo.Core.Data;
using Ceriyo.Core.Observables;
using Ceriyo.Core.Services.Contracts;
using Ceriyo.Infrastructure.WPF.BindableBases;
using Ceriyo.Infrastructure.WPF.Observables;
using Ceriyo.Toolset.WPF.Events.Class;
using Ceriyo.Toolset.WPF.Events.DataEditor;
using Ceriyo.Toolset.WPF.Events.Module;
using Prism.Commands;
using Prism.Events;
using Prism.Interactivity.InteractionRequest;

namespace Ceriyo.Toolset.WPF.Views.ClassEditorView
{
    public class ClassEditorViewModel : ValidatableBindableBase<ClassEditorViewModel>
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IDataService _dataService;
        private readonly IPathService _pathService;
        private readonly ClassDataObservable.Factory _classDataFactory;

        public ClassEditorViewModel(
            IObjectMapper objectMapper, 
            IEventAggregator eventAggregator,
            IDataService dataService,
            IPathService pathService,
            ClassEditorViewModelValidator validator,
            ClassDataObservable.Factory classDataFactory)
            :base(objectMapper, validator)
        {
            _eventAggregator = eventAggregator;
            _dataService = dataService;
            _pathService = pathService;
            _classDataFactory = classDataFactory;

            NewCommand = new DelegateCommand(New);
            DeleteCommand = new DelegateCommand(Delete);

            Classes = new ObservableCollectionEx<ClassDataObservable>();

            ConfirmDeleteRequest = new InteractionRequest<IConfirmation>();
            
            Classes.ItemPropertyChanged += ClassesOnItemPropertyChanged;

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
            Classes.Clear();
        }

        private void DataEditorClosed(bool doSave)
        {
            LoadExistingData();
        }

        private void LoadExistingData()
        {
            Classes.Clear();
            string[] files = Directory.GetFiles($"{_pathService.ModulesTempDirectory}Class/", "*.dat");

            foreach (var file in files)
            {
                ClassData loaded = _dataService.Load<ClassData>(file);
                ClassDataObservable @class = _classDataFactory.Invoke(loaded);
                Classes.Add(@class);
            }
        }

        private void RaiseValidityChangedEvent()
        {
            _eventAggregator.GetEvent<ClassEditorValidityChangedEvent>().Publish(!HasErrors);
        }

        private void ClassesOnItemPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            ClassDataObservable classChanged = (ClassDataObservable)sender;
            _eventAggregator.GetEvent<ClassChangedEvent>().Publish(classChanged);
            RaiseValidityChangedEvent();
        }

        private ObservableCollectionEx<ClassDataObservable> _classes;

        public ObservableCollectionEx<ClassDataObservable> Classes
        {
            get { return _classes; }
            set { SetProperty(ref _classes, value); }
        }

        private ClassDataObservable _selectedClass;
        public ClassDataObservable SelectedClass
        {
            get { return _selectedClass; }
            set
            {
                SetProperty(ref _selectedClass, value);
                OnPropertyChanged("IsClassSelected");
            }
        }
        
        public bool IsClassSelected => SelectedClass != null;


        public DelegateCommand NewCommand { get; set; }
        public DelegateCommand DeleteCommand { get; set; }

        private void New()
        {
            ClassDataObservable @class = _classDataFactory.Invoke();
            @class.Name = "Class" + (Classes.Count + 1);
            Classes.Add(@class);
            _eventAggregator.GetEvent<ClassCreatedEvent>().Publish(@class);
            RaiseValidityChangedEvent();
        }

        private void Delete()
        {
            ConfirmDeleteRequest.Raise(
                new Confirmation
                {
                    Title = "Delete Class?",
                    Content = "Are you sure you want to delete this class?"
                }, c =>
                {
                    if (!c.Confirmed) return;
                    _eventAggregator.GetEvent<ClassDeletedEvent>().Publish(SelectedClass);
                    Classes.Remove(SelectedClass);
                    RaiseValidityChangedEvent();
                });
        }
        
        public InteractionRequest<IConfirmation> ConfirmDeleteRequest { get; }
    }
}
