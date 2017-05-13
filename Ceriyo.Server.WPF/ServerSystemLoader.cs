using System;
using Artemis;
using Artemis.Attributes;
using Artemis.System;
using Ceriyo.Core.Contracts;
using Ceriyo.Core.Systems.Update;

namespace Ceriyo.Server.WPF
{
    public class ServerSystemLoader: ISystemLoader
    {
        private readonly EntityWorld _world;
        private readonly HeartbeatSystem _heartbeatSystem;

        public ServerSystemLoader(EntityWorld world,
            HeartbeatSystem heartbeatSystem)
        {
            _world = world;
            _heartbeatSystem = heartbeatSystem;
        }

        public void LoadSystems()
        {
            // Update Systems (Order Matters)
            RegisterSystem(_heartbeatSystem);
        }
        private void RegisterSystem(EntitySystem system)
        {
            Type type = system.GetType();
            ArtemisEntitySystem attribute = (ArtemisEntitySystem)type.GetCustomAttributes(typeof(ArtemisEntitySystem), true)[0];
            _world.SystemManager.SetSystem(system, attribute.GameLoopType, attribute.Layer, attribute.ExecutionType);
        }
    }
}
