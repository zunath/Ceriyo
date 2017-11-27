using System.Collections.Generic;
using Artemis.Interface;
using Ceriyo.Core.Constants;

namespace Ceriyo.Core.Components
{
    /// <summary>
    /// Tracks a group of scripts for an entity.
    /// </summary>
    public class ScriptGroup: Dictionary<ScriptEvent, string>, IComponent
    {
    }
}
