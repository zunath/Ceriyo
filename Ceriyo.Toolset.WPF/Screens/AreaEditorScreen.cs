using Artemis;
using Ceriyo.Core.Components;
using Ceriyo.Core.Constants;
using Ceriyo.Core.Contracts;
using Ceriyo.Core.Data;
using Ceriyo.Core.Entities;
using Ceriyo.Core.Services.Contracts;
using Ceriyo.Infrastructure.WPF.Observables;
using Ceriyo.Toolset.WPF.EventArgs;
using Ceriyo.Toolset.WPF.Events.Area;
using Ceriyo.Toolset.WPF.Events.Camera;
using Ceriyo.Toolset.WPF.Events.ObjectSelector;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using Prism.Events;

namespace Ceriyo.Toolset.WPF.Screens
{
    public class AreaEditorScreen: IScreen
    {
        private Entity _loadedArea;
        private Entity _objectPainter;
        private readonly IEventAggregator _eventAggregator;
        private readonly IEntityFactory _entityFactory;
        private readonly IObjectMapper _objectMapper;
        private readonly IModuleDataService _moduleDataService;
        private readonly IEngineService _engineService;
        private readonly Camera2D _camera;
        private AreaData _loadedAreaData;

        public AreaEditorScreen(IEventAggregator eventAggregator,
            IEntityFactory entityFactory,
            IObjectMapper objectMapper,
            IModuleDataService moduleDataService,
            IEngineService engineService,
            Camera2D camera)
        {
            _eventAggregator = eventAggregator;
            _entityFactory = entityFactory;
            _objectMapper = objectMapper;
            _camera = camera;
            _moduleDataService = moduleDataService;
            _engineService = engineService;

            _eventAggregator.GetEvent<AreaOpenedEvent>().Subscribe(AreaOpened);
            _eventAggregator.GetEvent<AreaClosedEvent>().Subscribe(AreaClosed);
            _eventAggregator.GetEvent<CameraMovedEvent>().Subscribe(CameraMoved);
            _eventAggregator.GetEvent<CameraZoomedEvent>().Subscribe(CameraZoomed);
            _eventAggregator.GetEvent<CameraResetEvent>().Subscribe(CameraReset);
            _eventAggregator.GetEvent<AreaPropertiesChangedEvent>().Subscribe(AreaPropertiesChanged);
            _eventAggregator.GetEvent<TileSelectedEvent>().Subscribe(TileSelected);
        }

        private void TileSelected(TileSelectedEventArgs e)
        {
            var renderable = _objectPainter.GetComponent<Renderable>();
            renderable.Source = new Rectangle(
                e.CellX * _engineService.TileWidth,
                e.CellY * _engineService.TileHeight,
                _engineService.TileWidth,
                _engineService.TileHeight);
        }

        private void AreaPropertiesChanged(AreaDataObservable area)
        {
            bool changed = false;
            AreaData updatedArea = _objectMapper.Map<AreaData>(area);

            if (updatedArea.Width != _loadedAreaData.Width ||
                updatedArea.Height != _loadedAreaData.Height)
            {
                var map = _loadedArea.GetComponent<Map>();
                map.Resize(updatedArea.Width, updatedArea.Height);
                var paintable = _objectPainter.GetComponent<Paintable>();
                paintable.AreaHeight = updatedArea.Height;
                paintable.AreaWidth = updatedArea.Width;

                changed = true;
            }

            if (updatedArea.TilesetGlobalID != _loadedAreaData.TilesetGlobalID)
            {
                // TODO: Blank out existing tiles and load new tileset spritesheet.
                changed = true;
            }

            if (changed)
            {
                _loadedAreaData = updatedArea;
            }
        }

        private void CameraReset()
        {
            _camera.Zoom = 1.0f;
            _camera.Position = Vector2.Zero;
        }

        private void CameraZoomed(Zoom zoom)
        {
            if (zoom == Zoom.In)
            {
                _camera.ZoomIn(1.0f);
            }
            else if (zoom == Zoom.Out)
            {
                _camera.ZoomOut(1.0f);
            }
        }

        private void CameraMoved(Direction direction)
        {
            if (direction == Direction.North)
            {
                _camera.Move(new Vector2(0, -20));
            }
            else if (direction == Direction.South)
            {
                _camera.Move(new Vector2(0, 20));
            }
            else if (direction == Direction.East)
            {
                _camera.Move(new Vector2(20, 0));
            }
            else if (direction == Direction.West)
            {
                _camera.Move(new Vector2(-20, 0));
            }
        }

        private void AreaClosed(AreaDataObservable area)
        {
            CameraReset();
            _loadedArea.Delete();
            _loadedAreaData = null;
            _objectPainter.Delete();
            _objectPainter = null;
        }

        private void AreaOpened(AreaDataObservable area)
        {
            CameraReset();
            _loadedAreaData = _objectMapper.Map<AreaData>(area);
            _loadedArea = _entityFactory.Create<Area, AreaData>(_loadedAreaData);

            Texture2D texture = _loadedArea.GetComponent<Renderable>().Texture;
            _objectPainter = _entityFactory.Create<ObjectPainter, Texture2D>(texture);
            Paintable paintable = _objectPainter.GetComponent<Paintable>();
            paintable.AreaHeight = _loadedAreaData.Height;
            paintable.AreaWidth = _loadedAreaData.Width;
        }

        public void Initialize()
        {
            _camera.MaximumZoom = 3.0f;
            _camera.MinimumZoom = 1.0f;
            _camera.Zoom = 1.0f;
        }

        public void Update()
        {
            
        }

        public void Draw()
        {
            
        }

        public void Close()
        {
            
        }
    }
}
