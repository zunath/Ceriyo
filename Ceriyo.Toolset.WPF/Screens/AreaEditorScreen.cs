using Artemis;
using Ceriyo.Core.Contracts;
using Ceriyo.Core.Data;
using Ceriyo.Core.Entities;
using Ceriyo.Infrastructure.WPF.Observables;
using Ceriyo.Toolset.WPF.Events.Area;
using Prism.Events;

namespace Ceriyo.Toolset.WPF.Screens
{
    public class AreaEditorScreen: IScreen
    {
        private Entity _loadedArea;
        private readonly IEventAggregator _eventAggregator;
        private readonly IEntityFactory _entityFactory;
        private readonly IObjectMapper _objectMapper;

        public AreaEditorScreen(IEventAggregator eventAggregator,
            IEntityFactory entityFactory,
            IObjectMapper objectMapper)
        {
            _eventAggregator = eventAggregator;
            _entityFactory = entityFactory;
            _objectMapper = objectMapper;

            _eventAggregator.GetEvent<AreaOpenedEvent>().Subscribe(AreaOpened);
            _eventAggregator.GetEvent<AreaClosedEvent>().Subscribe(AreaClosed);
        }

        private void AreaClosed(AreaDataObservable area)
        {
            _loadedArea.Delete();
        }

        private void AreaOpened(AreaDataObservable area)
        {
            AreaData data = _objectMapper.Map<AreaData>(area);
            _loadedArea = _entityFactory.Create<Area, AreaData>(data);
        }

        public void Initialize()
        {
            
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
