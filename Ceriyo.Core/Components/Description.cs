using Artemis.Interface;

namespace Ceriyo.Core.Components
{
    public class Description: IComponent
    {
        public string Value { get; set; }

        public Description()
        {
            Value = string.Empty;
        }
    }
}
