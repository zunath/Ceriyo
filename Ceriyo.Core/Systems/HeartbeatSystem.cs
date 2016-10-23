using Artemis;
using Artemis.Attributes;
using Artemis.Manager;
using Artemis.System;
using Ceriyo.Core.Components;
using Ceriyo.Core.Constants;
using Ceriyo.Core.Contracts;
using Ceriyo.Core.Extensions;

namespace Ceriyo.Core.Systems
{
    [ArtemisEntitySystem(
        ExecutionType = ExecutionType.Synchronous,
        GameLoopType = GameLoopType.Update)]
    public class HeartbeatSystem: EntityProcessingSystem
    {
        private readonly EntityWorld _world;
        private readonly IScriptService _scriptService;

        public HeartbeatSystem(EntityWorld world,
            IScriptService scriptService)
            : base(Aspect.All(typeof(Heartbeat),
                              typeof(ScriptGroup)))
        {
            _world = world;
            _scriptService = scriptService;
        }

        public override void Process(Entity entity)
        {
            ScriptGroup scripts = entity.GetComponent<ScriptGroup>();
            if (!scripts.ContainsKey(ScriptEvent.OnHeartbeat) ||
                 string.IsNullOrWhiteSpace(scripts[ScriptEvent.OnHeartbeat])) return;

            Heartbeat hb = entity.GetComponent<Heartbeat>();
            if (hb.Interval <= 0.0f) return;
            hb.CurrentTimer += _world.DeltaSeconds();
            if (hb.CurrentTimer < hb.Interval) return;

            _scriptService.QueueScript(scripts[ScriptEvent.OnHeartbeat], entity);
            hb.CurrentTimer = 0.0f;
        }

    }
}
