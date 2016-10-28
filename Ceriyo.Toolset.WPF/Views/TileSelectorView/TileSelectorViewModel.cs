using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Ceriyo.Core.Constants;
using Ceriyo.Core.Data;
using Ceriyo.Core.Services.Contracts;
using Ceriyo.Infrastructure.WPF.Helpers;
using Ceriyo.Infrastructure.WPF.Observables;
using Ceriyo.Toolset.WPF.Events.Area;
using Prism.Events;
using Prism.Mvvm;

namespace Ceriyo.Toolset.WPF.Views.TileSelectorView
{
    public class TileSelectorViewModel : BindableBase
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IModuleDataService _moduleDataService;
        private readonly IModuleResourceService _resourceService;
        private readonly IEngineService _engineService;

        public TileSelectorViewModel(IEventAggregator eventAggregator,
            IModuleDataService moduleDataService,
            IModuleResourceService resourceService,
            IEngineService engineService)
        {
            _eventAggregator = eventAggregator;
            _moduleDataService = moduleDataService;
            _resourceService = resourceService;
            _engineService = engineService;
            TileGridVisibility = Visibility.Hidden;

            SelectedTileBrush = Brushes.Yellow;
            SelectedTileHeight = _engineService.TileHeight;
            SelectedTileWidth = _engineService.TileWidth;
            SelectedTileVisibility = Visibility.Visible;
            SelectedTileX = 0;
            SelectedTileY = 0;

            _eventAggregator.GetEvent<AreaOpenedEvent>().Subscribe(AreaOpened);
            _eventAggregator.GetEvent<AreaClosedEvent>().Subscribe(AreaClosed);
        }

        private void AreaOpened(AreaDataObservable area)
        {
            TilesetData tileset = _moduleDataService.Load<TilesetData>(area.TilesetGlobalID);
            ResourceItemData data = _resourceService.GetResourceByName(ResourceType.Tileset, tileset.ResourceName);
            ActiveTilesetImage = BitmapImageHelpers.LoadFromBytes(data.Data);
            ActiveTilesetImageWidth = ActiveTilesetImage.Width;
            ActiveTilesetImageHeight = ActiveTilesetImage.Height;
            TileGridVisibility = Visibility.Visible;
        }

        private void AreaClosed(AreaDataObservable area)
        {
            ClearActiveTilesetImage();
            TileGridVisibility = Visibility.Hidden;
        }

        private BitmapImage _activeTilesetImage;

        public BitmapImage ActiveTilesetImage
        {
            get { return _activeTilesetImage; }
            set { SetProperty(ref _activeTilesetImage, value); }
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
        
        private void ClearActiveTilesetImage()
        {
            ActiveTilesetImage = null;
            ActiveTilesetImageWidth = 0.0;
            ActiveTilesetImageHeight = 0.0;
        }

        private Visibility _tileGridVisibility;

        public Visibility TileGridVisibility
        {
            get { return _tileGridVisibility; }
            set { SetProperty(ref _tileGridVisibility, value); }
        }

        private Brush _selectedTileBrush;

        public Brush SelectedTileBrush
        {
            get { return _selectedTileBrush; }
            set { SetProperty(ref _selectedTileBrush, value); }
        }

        private int _selectedTileHeight;

        public int SelectedTileHeight
        {
            get { return _selectedTileHeight; }
            set { SetProperty(ref _selectedTileHeight, value); }
        }

        private int _selectedTileWidth;

        public int SelectedTileWidth
        {
            get { return _selectedTileWidth; }
            set { SetProperty(ref _selectedTileWidth, value); }
        }

        private Visibility _selectedTileVisibility;

        public Visibility SelectedTileVisibility
        {
            get { return _selectedTileVisibility; }
            set { SetProperty(ref _selectedTileVisibility, value); }
        }

        private int _selectedTileX;

        public int SelectedTileX
        {
            get { return _selectedTileX; }
            set { SetProperty(ref _selectedTileX, value); }
        }

        private int _selectedTileY;

        public int SelectedTileY
        {
            get { return _selectedTileY; }
            set { SetProperty(ref _selectedTileY, value); }
        }

    }
}
