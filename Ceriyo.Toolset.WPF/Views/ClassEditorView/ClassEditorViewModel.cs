using System.ComponentModel;
using System.IO;
using Ceriyo.Core.Contracts;
using Ceriyo.Core.Data;
using Ceriyo.Core.Extensions;
using Ceriyo.Toolset.WPF.Events.Class;
using Ceriyo.Toolset.WPF.Events.DataEditor;
using Ceriyo.Toolset.WPF.Events.Module;
using Prism.Commands;
using Prism.Events;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;

namespace Ceriyo.Toolset.WPF.Views.ClassEditorView
{
    public class ClassEditorViewModel : BindableBase
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IDataService _dataService;

        public ClassEditorViewModel(IEventAggregator eventAggregator,
            IDataService dataService)
        {
            _eventAggregator = eventAggregator;
            _dataService = dataService;

            NewCommand = new DelegateCommand(New);
            DeleteCommand = new DelegateCommand(Delete);

            Classes = new ObservableCollectionEx<ClassData>();

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
            string[] files = Directory.GetFiles("./Modules/temp0/Class/", "*.dat");

            foreach (var file in files)
            {
                Classes.Add(_dataService.Load<ClassData>(file));
            }
        }

        private void ClassesOnItemPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            ClassData classChanged = sender as ClassData;
            
            _eventAggregator.GetEvent<ClassChangedEvent>().Publish(classChanged);
        }

        private ObservableCollectionEx<ClassData> _classes;

        public ObservableCollectionEx<ClassData> Classes
        {
            get { return _classes; }
            set { SetProperty(ref _classes, value); }
        }

        private ClassData _selectedClass;
        public ClassData SelectedClass
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
            ClassData @class = new ClassData
            {
                Name = "Class" + (Classes.Count + 1)
            };
            Classes.Add(@class);

            _eventAggregator.GetEvent<ClassCreatedEvent>().Publish(@class);
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
                });
        }
        
        public InteractionRequest<IConfirmation> ConfirmDeleteRequest { get; }

    }
}
