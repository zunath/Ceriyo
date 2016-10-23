using Artemis;
using Artemis.Attributes;
using Artemis.Manager;
using Artemis.System;
using Ceriyo.Core.Components;

namespace Ceriyo.Core.Systems
{
    [ArtemisEntitySystem(
        ExecutionType = ExecutionType.Synchronous,
        GameLoopType = GameLoopType.Draw,
        Layer = 1)]
    public class AreaRenderSystem: EntityProcessingSystem
    {
        public AreaRenderSystem() 
            : base(Aspect.All(typeof(Renderable),
                              typeof(Map)))
        {
        }

        public override void Process(Entity entity)
        {

        }
    }
}
