using System;
using Artemis;
using Ceriyo.Core.Components;
using Ceriyo.Core.Scripting.Server.Contracts;

namespace Ceriyo.Core.Scripting.Server
{
    public class EntityMethods : IEntityMethods
    {
        public string GetName(Entity entity)
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
