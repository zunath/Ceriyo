using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using Ceriyo.Core.Contracts;
using Ceriyo.Core.Data;
using Ceriyo.Core.Observables;
using Ceriyo.Core.Services.Contracts;
using Ceriyo.Infrastructure.WPF.BindableBases;
using Ceriyo.Infrastructure.WPF.Factory.Contracts;
using Ceriyo.Infrastructure.WPF.Observables;
using Ceriyo.Toolset.WPF.Events.DataEditor;
using Ceriyo.Toolset.WPF.Events.Module;
using Ceriyo.Toolset.WPF.Events.Placeable;
using Prism.Commands;
using Prism.Events;
using Prism.Interactivity.InteractionRequest;

namespace Ceriyo.Toolset.WPF.Views.PlaceableEditorView
{
    public class PlaceableEditorViewModel : ValidatableBindableBase<PlaceableEditorViewModelValidator>
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IDataService _dataService;
        private readonly IPathService _pathService;
        private readonly IObservableDataFactory _observableDataFactory;

        public PlaceableEditorViewModel(
            IEventAggregator eventAggregator,
            IDataService dataService,
            IPathService pathService,
            IObservableDataFactory observableDataFactory)
        {
            _eventAggregator = eventAggregator;
            _dataService = dataService;
            _pathService = pathService;
            _observableDataFactory = observableDataFactory;

            NewCommand = new DelegateCommand(New);
            DeleteCommand = new DelegateCommand(Delete);

            AddLocalStringCommand = new DelegateCommand(AddLocalString);
            AddLocalDoubleCommand = new DelegateCommand(AddLocalDouble);
            DeleteLocalStringCommand = new DelegateCommand<LocalStringDataObservable>(DeleteLocalString);
            DeleteLocalDoubleCommand = new DelegateCommand<LocalDoubleDataObservable>(DeleteLocalDouble);

            Placeables = new ObservableCollectionEx<PlaceableDataObservable>();
            Scripts = new Dictionary<string, ScriptDataObservable>();
            
            ConfirmDeleteRequest = new InteractionRequest<IConfirmation>();
            
            Placeables.ItemPropertyChanged += PlaceablesOnItemPropertyChanged;

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
            Placeables.Clear();
        }

        private void DataEditorClosed(bool doSave)
        {
            LoadExistingData();
        }

        private void LoadExistingData()
        {
            Placeables.Clear();
            string[] files = Directory.GetFiles($"{_pathService.ModulesTempDirectory}Placeable/", "*.dat");

            foreach (var file in files)
            {
                PlaceableData loaded = _dataService.Load<PlaceableData>(file);
                PlaceableDataObservable placeable = _observableDataFactory.CreateAndMap<PlaceableDataObservable, PlaceableData>(loaded);
                Placeables.Add(placeable);
            }
        }

        private void RaiseValidityChangedEvent()
        {
            ValidateObject();
            _eventAggregator.GetEvent<PlaceableEditorValidityChangedEvent>().Publish(!HasErrors);
        }

        private void PlaceablesOnItemPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            PlaceableDataObservable placeableChanged = (PlaceableDataObservable)sender;
            _eventAggregator.GetEvent<PlaceableChangedEvent>().Publish(placeableChanged);
            RaiseValidityChangedEvent();
        }

        private ObservableCollectionEx<PlaceableDataObservable> _placeables;

        public ObservableCollectionEx<PlaceableDataObservable> Placeables
        {
            get { return _placeables; }
            set { SetProperty(ref _placeables, value); }
        }

        private PlaceableDataObservable _selectedPlaceable;
        public PlaceableDataObservable SelectedPlaceable
        {
            get { return _selectedPlaceable; }
            set
            {
                SetProperty(ref _selectedPlaceable, value);
                OnPropertyChanged(nameof(IsPlaceableSelected));
            }
        }

        private Dictionary<string, ScriptDataObservable> _scripts;

        public Dictionary<string, ScriptDataObservable> Scripts
        {
            get { return _scripts; }
            set { SetProperty(ref _scripts, value); }
        }


        public bool IsPlaceableSelected => SelectedPlaceable != null;


        public DelegateCommand NewCommand { get; set; }
        public DelegateCommand DeleteCommand { get; set; }

        private void New()
        {
            var placeable = _observableDataFactory.Create<PlaceableDataObservable>();
            placeable.Name = "Placeable" + (Placeables.Count + 1);
            Placeables.Add(placeable);

            _eventAggregator.GetEvent<PlaceableCreatedEvent>().Publish(placeable);
            RaiseValidityChangedEvent();
        }

        private void Delete()
        {
            ConfirmDeleteRequest.Raise(
                new Confirmation
                {
                    Title = "Delete Placeable?",
                    Content = "Are you sure you want to delete this placeable?"
                }, c =>
                {
                    if (!c.Confirmed) return;
                    _eventAggregator.GetEvent<PlaceableDeletedEvent>().Publish(SelectedPlaceable);
                    Placeables.Remove(SelectedPlaceable);
                    RaiseValidityChangedEvent();
                });
        }

        public DelegateCommand AddLocalStringCommand { get; }

        private void AddLocalString()
        {
            SelectedPlaceable.LocalVariables.LocalStrings.Add(_observableDataFactory.Create<LocalStringDataObservable>());
        }

        public DelegateCommand<LocalStringDataObservable> DeleteLocalStringCommand { get; }

        private void DeleteLocalString(LocalStringDataObservable localString)
        {
            SelectedPlaceable.LocalVariables.LocalStrings.Remove(localString);
        }

        public DelegateCommand AddLocalDoubleCommand { get; }

        private void AddLocalDouble()
        {
            SelectedPlaceable.LocalVariables.LocalDoubles.Add(_observableDataFactory.Create<LocalDoubleDataObservable>());
        }

        public DelegateCommand<LocalDoubleDataObservable> DeleteLocalDoubleCommand { get; }

        private void DeleteLocalDouble(LocalDoubleDataObservable localDouble)
        {
            SelectedPlaceable.LocalVariables.LocalDoubles.Remove(localDouble);
        }
        public InteractionRequest<IConfirmation> ConfirmDeleteRequest { get; }
    }
}
