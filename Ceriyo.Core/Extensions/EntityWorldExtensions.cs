using System;
using Artemis;

namespace Ceriyo.Core.Extensions
{
    public static class EntityWorldExtensions
    {
        public static float DeltaSeconds(this EntityWorld world)
        {
            return TimeSpan.FromTicks(world.Delta).Milliseconds * 0.001f;
        }
    }
}
