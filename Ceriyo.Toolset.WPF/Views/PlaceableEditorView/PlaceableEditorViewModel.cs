using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using Ceriyo.Core.Contracts;
using Ceriyo.Core.Data;
using Ceriyo.Core.Extensions;
using Ceriyo.Core.Observables;
using Ceriyo.Core.Services.Contracts;
using Ceriyo.Toolset.WPF.Events.DataEditor;
using Ceriyo.Toolset.WPF.Events.Module;
using Ceriyo.Toolset.WPF.Events.Placeable;
using Prism.Commands;
using Prism.Events;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;

namespace Ceriyo.Toolset.WPF.Views.PlaceableEditorView
{
    public class PlaceableEditorViewModel : BindableBase
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IDataService _dataService;
        private readonly IPathService _pathService;

        public PlaceableEditorViewModel(IEventAggregator eventAggregator,
            IDataService dataService,
            IPathService pathService)
        {
            _eventAggregator = eventAggregator;
            _dataService = dataService;
            _pathService = pathService;

            NewCommand = new DelegateCommand(New);
            DeleteCommand = new DelegateCommand(Delete);

            Placeables = new ObservableCollectionEx<PlaceableData>();
            Scripts = new Dictionary<string, ScriptData>();

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
                Placeables.Add(_dataService.Load<PlaceableData>(file));
            }
        }
        private void PlaceablesOnItemPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            PlaceableData placeableChanged = sender as PlaceableData;
            _eventAggregator.GetEvent<PlaceableChangedEvent>().Publish(placeableChanged);
        }

        private ObservableCollectionEx<PlaceableData> _placeables;

        public ObservableCollectionEx<PlaceableData> Placeables
        {
            get { return _placeables; }
            set { SetProperty(ref _placeables, value); }
        }

        private PlaceableData _selectedPlaceable;
        public PlaceableData SelectedPlaceable
        {
            get { return _selectedPlaceable; }
            set
            {
                SetProperty(ref _selectedPlaceable, value);
                OnPropertyChanged("IsPlaceableSelected");
            }
        }

        private Dictionary<string, ScriptData> _scripts;

        public Dictionary<string, ScriptData> Scripts
        {
            get { return _scripts; }
            set { SetProperty(ref _scripts, value); }
        }


        public bool IsPlaceableSelected => SelectedPlaceable != null;


        public DelegateCommand NewCommand { get; set; }
        public DelegateCommand DeleteCommand { get; set; }

        private void New()
        {
            PlaceableData placeable = new PlaceableData
            {
                Name = "Placeable" + (Placeables.Count + 1)
            };
            Placeables.Add(placeable);

            _eventAggregator.GetEvent<PlaceableCreatedEvent>().Publish(placeable);
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
                });
        }

        public InteractionRequest<IConfirmation> ConfirmDeleteRequest { get; }

    }
}
