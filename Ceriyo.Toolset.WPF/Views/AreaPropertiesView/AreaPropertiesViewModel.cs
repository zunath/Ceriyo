using System;
using Ceriyo.Core.Contracts;
using Ceriyo.Core.Observables;
using Ceriyo.Infrastructure.WPF.BindableBases;
using Ceriyo.Infrastructure.WPF.Observables;
using Prism.Commands;
using Prism.Events;
using Prism.Interactivity.InteractionRequest;

namespace Ceriyo.Toolset.WPF.Views.AreaPropertiesView
{
    public class AreaPropertiesViewModel : ValidatableBindableBase<AreaPropertiesViewModel>, IInteractionRequestAware
    {
        private readonly IEventAggregator _eventAggregator;

        public AreaPropertiesViewModel(
            IEventAggregator eventAggregator,
            IObjectMapper mapper,
            AreaPropertiesViewModelValidator validator)
            : base(mapper, validator)
        {
            _eventAggregator = eventAggregator;

            Tilesets = new ObservableCollectionEx<TilesetDataObservable>();
            OpenInAreaViewer = true;
            AreaWidth = 8;
            AreaHeight = 8;

            CreateAreaCommand = new DelegateCommand(CreateArea);
            CancelCommand = new DelegateCommand(Cancel);

        }

        private string _name;

        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        private string _tag;

        public string Tag
        {
            get { return _tag; }
            set { SetProperty(ref _tag, value); }
        }

        private string _resref;

        public string Resref
        {
            get { return _resref; }
            set { SetProperty(ref _resref, value); }
        }

        private ObservableCollectionEx<TilesetDataObservable> _tilesets;

        public ObservableCollectionEx<TilesetDataObservable> Tilesets
        {
            get { return _tilesets; }
            set { SetProperty(ref _tilesets, value); }
        }

        private TilesetDataObservable _selectedTileset;

        public TilesetDataObservable SelectedTileset
        {
            get { return _selectedTileset; }
            set { SetProperty(ref _selectedTileset, value); }
        }

        private int _areaWidth;

        public int AreaWidth
        {
            get { return _areaWidth; }
            set { SetProperty(ref _areaWidth, value); }
        }

        private int _areaHeight;

        public int AreaHeight
        {
            get { return _areaHeight; }
            set { SetProperty(ref _areaHeight, value); }
        }

        private bool _launchAreaProperties;

        public bool LaunchAreaProperties
        {
            get { return _launchAreaProperties; }
            set { SetProperty(ref _launchAreaProperties, value); }
        }

        private bool _openInAreaViewer;

        public bool OpenInAreaViewer
        {
            get { return _openInAreaViewer; }
            set { SetProperty(ref _openInAreaViewer, value); }
        }

        public DelegateCommand CreateAreaCommand { get; }

        private void CreateArea()
        {
            
        }

        public DelegateCommand CancelCommand { get; }

        private void Cancel()
        {
            
        }

        public INotification Notification { get; set; }
        public Action FinishInteraction { get; set; }
    }
}
