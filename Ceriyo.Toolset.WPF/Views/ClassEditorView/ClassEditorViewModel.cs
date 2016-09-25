using System.ComponentModel;
using Ceriyo.Core.Data;
using Ceriyo.Core.Extensions;
using Ceriyo.Toolset.WPF.Events;
using Prism.Commands;
using Prism.Events;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;

namespace Ceriyo.Toolset.WPF.Views.ClassEditorView
{
    public class ClassEditorViewModel : BindableBase
    {
        private readonly IEventAggregator _eventAggregator;

        public ClassEditorViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;

            NewCommand = new DelegateCommand(New);
            DeleteCommand = new DelegateCommand(Delete);

            Classes = new ObservableCollectionEx<ClassData>();

            ConfirmDeleteRequest = new InteractionRequest<IConfirmation>();

            _eventAggregator.GetEvent<DataEditorClosedEvent>().Subscribe(DataEditorClosed);

            Classes.ItemPropertyChanged += ClassesOnItemPropertyChanged;
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
                    string globalID = SelectedClass.GlobalID;
                    Classes.Remove(SelectedClass);
                    _eventAggregator.GetEvent<ClassDeletedEvent>().Publish(globalID);
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
