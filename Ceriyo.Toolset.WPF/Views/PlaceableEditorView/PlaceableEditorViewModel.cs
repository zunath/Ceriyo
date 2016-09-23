using System.Collections.Generic;
using System.ComponentModel;
using Ceriyo.Core.Data;
using Ceriyo.Core.Extensions;
using Ceriyo.Toolset.WPF.Events;
using Prism.Commands;
using Prism.Events;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;

namespace Ceriyo.Toolset.WPF.Views.PlaceableEditorView
{
    public class PlaceableEditorViewModel : BindableBase
    {
        private readonly IEventAggregator _eventAggregator;

        public PlaceableEditorViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;

            NewCommand = new DelegateCommand(New);
            DeleteCommand = new DelegateCommand(Delete);

            Placeables = new ObservableCollectionEx<PlaceableData>();
            Scripts = new Dictionary<string, ScriptData>();

            ConfirmDeleteRequest = new InteractionRequest<IConfirmation>();

            _eventAggregator.GetEvent<DataEditorClosedEvent>().Subscribe(DataEditorClosed);

            Placeables.ItemPropertyChanged += PlaceablesOnItemPropertyChanged;
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
                    string globalID = SelectedPlaceable.GlobalID;
                    Placeables.Remove(SelectedPlaceable);
                    _eventAggregator.GetEvent<PlaceableDeletedEvent>().Publish(globalID);
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
