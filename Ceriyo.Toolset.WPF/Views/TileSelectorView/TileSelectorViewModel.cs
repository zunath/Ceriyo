using System.Windows;
using System.Windows.Media.Imaging;
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

        public TileSelectorViewModel(IEventAggregator eventAggregator,
            IModuleDataService moduleDataService,
            IModuleResourceService resourceService)
        {
            _eventAggregator = eventAggregator;
            _moduleDataService = moduleDataService;
            _resourceService = resourceService;
            TileGridVisibility = Visibility.Hidden;
            
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

    }
}
