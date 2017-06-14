using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Media.Imaging;
using Ceriyo.Core.Constants;
using Ceriyo.Core.Data;
using Ceriyo.Core.Observables;
using Ceriyo.Core.Services.Contracts;
using Ceriyo.Infrastructure.WPF.BindableBases;
using Ceriyo.Infrastructure.WPF.Factory.Contracts;
using Ceriyo.Infrastructure.WPF.Helpers;
using Ceriyo.Infrastructure.WPF.Observables;
using Ceriyo.Toolset.WPF.Events.DataEditor;
using Ceriyo.Toolset.WPF.Events.Module;
using Ceriyo.Toolset.WPF.Events.Tileset;

using Prism.Commands;
using Prism.Events;
using Prism.Interactivity.InteractionRequest;

namespace Ceriyo.Toolset.WPF.Views.TilesetEditorView
{
    public class TilesetEditorViewModel : ValidatableBindableBase<TilesetEditorViewModelValidator>
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IModuleResourceService _resourceDomainService;
        private readonly IObservableDataFactory _observableDataFactory;
        private readonly IModuleDataService _moduleDataService;

        public TilesetEditorViewModel(
            IEventAggregator eventAggregator,
            IModuleResourceService resourceDomainService,
            IObservableDataFactory observableDataFactory,
            IModuleDataService moduleDataService)
        {
            _eventAggregator = eventAggregator;
            _resourceDomainService = resourceDomainService;
            _observableDataFactory = observableDataFactory;
            _moduleDataService = moduleDataService;

            NewCommand = new DelegateCommand(New);
            DeleteCommand = new DelegateCommand(Delete);

            Tilesets = new ObservableCollectionEx<TilesetDataObservable>();
            TilesetImages = new ObservableCollectionEx<string>();
            ActiveTilesetImage = new BitmapImage();
            
            ConfirmDeleteRequest = new InteractionRequest<IConfirmation>();
            Tilesets.ItemPropertyChanged += TilesetsOnItemPropertyChanged;

            _eventAggregator.GetEvent<ModuleLoadedEvent>().Subscribe(ModuleLoaded);
            _eventAggregator.GetEvent<DataEditorClosedEvent>().Subscribe(DataEditorClosed);
            _eventAggregator.GetEvent<ModuleClosedEvent>().Subscribe(ModuleClosed);
        }


        private void ModuleLoaded(string moduleFileName)
        {
            LoadExistingData();
            LoadTilesetImages();
        }

        private void ModuleClosed()
        {
            Tilesets.Clear();
            TilesetImages.Clear();
        }

        private void DataEditorClosed(bool doSave)
        {
            LoadExistingData();
            LoadTilesetImages();
            ClearActiveTilesetImage();
        }

        private void LoadExistingData()
        {
            Tilesets.Clear();
            foreach (var loaded in _moduleDataService.LoadAll<TilesetData>())
            {
                var tileset = _observableDataFactory.CreateAndMap<TilesetDataObservable, TilesetData>(loaded);
                Tilesets.Add(tileset);
            }
        }

        private void LoadTilesetImages()
        {
            TilesetImages.Clear();

            TilesetImages.Add(string.Empty);
            var resources = _resourceDomainService.GetResourceNamesByType(ResourceType.Tileset);
            foreach (var resource in resources)
            {
                TilesetImages.Add(resource);
            }
        }

        private void RaiseValidityChangedEvent()
        {
            ValidateObject();
            _eventAggregator.GetEvent<TilesetEditorValidityChangedEvent>().Publish(!HasErrors);
        }

        private void TilesetsOnItemPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            TilesetDataObservable tileset = (TilesetDataObservable)sender;
            _eventAggregator.GetEvent<TilesetChangedEvent>().Publish(tileset);
            RaiseValidityChangedEvent();
            LoadActiveTilesetImage();
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
            set
            {
                SetProperty(ref _selectedTileset, value);
                RaisePropertyChanged(nameof(IsTilesetSelected));
                LoadActiveTilesetImage();
            }
        }

        private Dictionary<string, ScriptDataObservable> _scripts;

        public Dictionary<string, ScriptDataObservable> Scripts
        {
            get { return _scripts; }
            set { SetProperty(ref _scripts, value); }
        }


        public bool IsTilesetSelected => SelectedTileset != null;


        public DelegateCommand NewCommand { get; set; }
        public DelegateCommand DeleteCommand { get; set; }

        private void New()
        {
            TilesetDataObservable tileset = _observableDataFactory.Create<TilesetDataObservable>();
            tileset.Name = "Tileset" + (Tilesets.Count + 1);
            Tilesets.Add(tileset);

            _eventAggregator.GetEvent<TilesetCreatedEvent>().Publish(tileset);
            RaiseValidityChangedEvent();
        }

        private void Delete()
        {
            ConfirmDeleteRequest.Raise(
                new Confirmation
                {
                    Title = "Delete Tileset?",
                    Content = "Are you sure you want to delete this tileset?"
                }, c =>
                {
                    if (!c.Confirmed) return;
                    _eventAggregator.GetEvent<TilesetDeletedEvent>().Publish(SelectedTileset);
                    Tilesets.Remove(SelectedTileset);
                    RaiseValidityChangedEvent();
                });
        }

        private ObservableCollectionEx<string> _tilesetImages;

        public ObservableCollectionEx<string> TilesetImages
        {
            get { return _tilesetImages; }
            set { SetProperty(ref _tilesetImages, value); }
        }

        private BitmapImage _activeTilesetImage;

        public BitmapImage ActiveTilesetImage
        {
            get { return _activeTilesetImage; }
            set { SetProperty(ref _activeTilesetImage, value); }
        }

        private void LoadActiveTilesetImage()
        {
            if (string.IsNullOrWhiteSpace(SelectedTileset?.ResourceName))
            {
                ClearActiveTilesetImage();
                return;
            }

            var resource = _resourceDomainService.GetResourceByName(ResourceType.Tileset, SelectedTileset.ResourceName);
            if (resource == null)
            {
                ClearActiveTilesetImage();
                return;
            }

            ActiveTilesetImage = BitmapImageHelpers.LoadFromBytes(resource.Data);
            ActiveTilesetImageWidth = ActiveTilesetImage.Width;
            ActiveTilesetImageHeight = ActiveTilesetImage.Height;
        }

        private void ClearActiveTilesetImage()
        {
            ActiveTilesetImage = null;
            ActiveTilesetImageWidth = 0.0;
            ActiveTilesetImageHeight = 0.0;
        }

        private double _activeTilesetImageWidth;

        public double ActiveTilesetImageWidth
        {
            get { return _activeTilesetImageWidth; }
            set { SetProperty(ref _activeTilesetImageWidth, value); }
        }

        private double _activeTilesetImageHeight;

        public double ActiveTilesetImageHeight
        {
            get { return _activeTilesetImageHeight; }
            set { SetProperty(ref _activeTilesetImageHeight, value); }
        }

        public InteractionRequest<IConfirmation> ConfirmDeleteRequest { get; }

    }
}
