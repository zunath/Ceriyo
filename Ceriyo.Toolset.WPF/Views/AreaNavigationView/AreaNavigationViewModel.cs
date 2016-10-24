using Ceriyo.Core.Constants;
using Ceriyo.Infrastructure.WPF.Observables;
using Ceriyo.Toolset.WPF.Events.Area;
using Ceriyo.Toolset.WPF.Events.Camera;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;

namespace Ceriyo.Toolset.WPF.Views.AreaNavigationView
{
    public class AreaNavigationViewModel : BindableBase
    {
        private readonly IEventAggregator _eventAggregator;

        public AreaNavigationViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;

            MoveCameraLeftCommand = new DelegateCommand(MoveCameraLeft);
            MoveCameraRightCommand = new DelegateCommand(MoveCameraRight);
            MoveCameraUpCommand = new DelegateCommand(MoveCameraUp);
            MoveCameraDownCommand = new DelegateCommand(MoveCameraDown);
            ZoomInCameraCommand = new DelegateCommand(ZoomInCamera);
            ZoomOutCameraCommand = new DelegateCommand(ZoomOutCamera);
            ResetCameraCommand = new DelegateCommand(ResetCamera);

            _eventAggregator.GetEvent<AreaOpenedEvent>().Subscribe(AreaOpened);
            _eventAggregator.GetEvent<AreaClosedEvent>().Subscribe(AreaClosed);
        }

        private void AreaClosed(AreaDataObservable obj)
        {
            IsAreaOpened = false;
        }

        private void AreaOpened(AreaDataObservable area)
        {
            IsAreaOpened = true;
        }

        private bool _isAreaOpened;

        public bool IsAreaOpened
        {
            get { return _isAreaOpened; }
            set { SetProperty(ref _isAreaOpened, value); }
        }

        public DelegateCommand MoveCameraLeftCommand { get; }

        private void MoveCameraLeft()
        {
            _eventAggregator.GetEvent<CameraMovedEvent>().Publish(Direction.West);
        }

        public DelegateCommand MoveCameraRightCommand { get; }

        private void MoveCameraRight()
        {
            _eventAggregator.GetEvent<CameraMovedEvent>().Publish(Direction.East);
        }

        public DelegateCommand MoveCameraUpCommand { get; }

        private void MoveCameraUp()
        {
            _eventAggregator.GetEvent<CameraMovedEvent>().Publish(Direction.North);
        }

        public DelegateCommand MoveCameraDownCommand { get; }

        private void MoveCameraDown()
        {
            _eventAggregator.GetEvent<CameraMovedEvent>().Publish(Direction.South);
        }

        public DelegateCommand ZoomInCameraCommand { get; }

        private void ZoomInCamera()
        {
            _eventAggregator.GetEvent<CameraZoomedEvent>().Publish(Zoom.In);
        }

        public DelegateCommand ZoomOutCameraCommand { get; }

        private void ZoomOutCamera()
        {
            _eventAggregator.GetEvent<CameraZoomedEvent>().Publish(Zoom.Out);
        }

        public DelegateCommand ResetCameraCommand { get; }

        private void ResetCamera()
        {
            _eventAggregator.GetEvent<CameraResetEvent>().Publish();
        }

    }
}
