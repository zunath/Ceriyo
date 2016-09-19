using Artemis.Interface;

namespace Ceriyo.Core.Components
{
    public class Resref: IComponent
    {
        public string Value { get; set; }

        public Resref()
        {
            Value = string.Empty;
        }
    }
}
