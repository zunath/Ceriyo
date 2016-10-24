using Artemis;
using Ceriyo.Core.Constants;
using Ceriyo.Core.Contracts;
using Ceriyo.Core.Data;
using Ceriyo.Core.Entities;
using Ceriyo.Infrastructure.WPF.Observables;
using Ceriyo.Toolset.WPF.Events.Area;
using Ceriyo.Toolset.WPF.Events.Camera;
using Microsoft.Xna.Framework;
using MonoGame.Extended;
using Prism.Events;

namespace Ceriyo.Toolset.WPF.Screens
{
    public class AreaEditorScreen: IScreen
    {
        private Entity _loadedArea;
        private readonly IEventAggregator _eventAggregator;
        private readonly IEntityFactory _entityFactory;
        private readonly IObjectMapper _objectMapper;
        private readonly Camera2D _camera;

        public AreaEditorScreen(IEventAggregator eventAggregator,
            IEntityFactory entityFactory,
            IObjectMapper objectMapper,
            Camera2D camera)
        {
            _eventAggregator = eventAggregator;
            _entityFactory = entityFactory;
            _objectMapper = objectMapper;
            _camera = camera;

            _eventAggregator.GetEvent<AreaOpenedEvent>().Subscribe(AreaOpened);
            _eventAggregator.GetEvent<AreaClosedEvent>().Subscribe(AreaClosed);
            _eventAggregator.GetEvent<CameraMovedEvent>().Subscribe(CameraMoved);
            _eventAggregator.GetEvent<CameraZoomedEvent>().Subscribe(CameraZoomed);
            _eventAggregator.GetEvent<CameraResetEvent>().Subscribe(CameraReset);
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
        }

        private void AreaOpened(AreaDataObservable area)
        {
            CameraReset();
            AreaData data = _objectMapper.Map<AreaData>(area);
            _loadedArea = _entityFactory.Create<Area, AreaData>(data);
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
