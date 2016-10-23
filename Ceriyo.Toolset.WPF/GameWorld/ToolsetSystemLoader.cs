using System;
using Artemis;
using Artemis.Attributes;
using Artemis.System;
using Ceriyo.Core.Contracts;
using Ceriyo.Core.Systems;

namespace Ceriyo.Toolset.WPF.GameWorld
{
    public class ToolsetSystemLoader: ISystemLoader
    {
        private readonly EntityWorld _world;
        private readonly AreaRenderSystem _areaRenderSystem;

        public ToolsetSystemLoader(EntityWorld world,
            AreaRenderSystem areaRenderSystem)
        {
            _world = world;
            _areaRenderSystem = areaRenderSystem;
        }

        public void LoadSystems()
        {
            RegisterSystem(_areaRenderSystem);
        }

        private void RegisterSystem(EntitySystem system)
        {
            Type type = system.GetType();
            ArtemisEntitySystem attribute = (ArtemisEntitySystem)type.GetCustomAttributes(typeof(ArtemisEntitySystem), true)[0];
            _world.SystemManager.SetSystem(system, attribute.GameLoopType, attribute.Layer, attribute.ExecutionType);
        }
    }
}
