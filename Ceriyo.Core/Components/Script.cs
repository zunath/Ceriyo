using Artemis.Interface;
using Ceriyo.Core.Constants;

namespace Ceriyo.Core.Components
{
    public class Script: IComponent
    {
        public string Name { get; set; }
        public ScriptEvent Event { get; set; }
    }
}
