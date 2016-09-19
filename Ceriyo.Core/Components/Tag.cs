using Artemis.Interface;

namespace Ceriyo.Core.Components
{
    public class Tag: IComponent
    {
        public string Value { get; set; }
        
        public Tag()
        {
            Value = string.Empty;
        }
    }
}
