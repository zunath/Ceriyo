using Artemis;
using Artemis.Attributes;
using Artemis.Manager;
using Artemis.System;

namespace Ceriyo.Core.Systems
{
    [ArtemisEntitySystem(
        ExecutionType = ExecutionType.Synchronous,
        GameLoopType = GameLoopType.Draw,
        Layer = 1)]
    public class AreaRenderSystem: EntityProcessingSystem
    {
        public AreaRenderSystem(Aspect aspect) 
            : base(aspect)
        {
        }

        public override void Process(Entity entity)
        {

        }
    }
}
