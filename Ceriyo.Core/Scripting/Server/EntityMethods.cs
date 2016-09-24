using System;
using Artemis;
using Ceriyo.Core.Components;
using Ceriyo.Core.Contracts;

namespace Ceriyo.Core.Scripting.Server
{
    public class EntityMethods : IServerScriptMethodGroup
    {
        public static string GetName(Entity entity)
        {
            try
            {
                return entity.GetComponent<Nameable>().Name;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
    }
}
