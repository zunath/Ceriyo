using Artemis.Interface;

namespace Ceriyo.Core.Components
{
    public class Nameable: IComponent
    {
        /// <summary>
        /// The actual name.
        /// </summary>
        public string Value { get; set; }
    }
}
