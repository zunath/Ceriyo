using System;
using Artemis;
using Artemis.Attributes;
using Artemis.System;
using Ceriyo.Core.Contracts;
using Ceriyo.Core.Systems.Draw;
using Ceriyo.Core.Systems.Update;

namespace Ceriyo.Toolset.WPF.GameWorld
{
    public class ToolsetSystemLoader: ISystemLoader
    {
        private readonly EntityWorld _world;
        private readonly AreaRenderSystem _areaRenderSystem;
        private readonly RenderSystem _renderSystem;
        private readonly PainterSystem _painterSystem;

        public ToolsetSystemLoader(EntityWorld world,
            AreaRenderSystem areaRenderSystem,
            RenderSystem renderSystem,
            PainterSystem painterSystem)
        {
            _world = world;
            _areaRenderSystem = areaRenderSystem;
            _renderSystem = renderSystem;
            _painterSystem = painterSystem;
        }

        public void LoadSystems()
        {
            RegisterSystem(_areaRenderSystem);
            RegisterSystem(_renderSystem);
            RegisterSystem(_painterSystem);
        }

        private void RegisterSystem(EntitySystem system)
        {
            Type type = system.GetType();
            ArtemisEntitySystem attribute = (ArtemisEntitySystem)type.GetCustomAttributes(typeof(ArtemisEntitySystem), true)[0];
            _world.SystemManager.SetSystem(system, attribute.GameLoopType, attribute.Layer, attribute.ExecutionType);
        }
    }
}
