using Artemis.Interface;

namespace Ceriyo.Core.Components
{
    /// <summary>
    /// Tracks the name of an entity.
    /// </summary>
    public class Nameable: IComponent
    {
        /// <summary>
        /// The actual name.
        /// </summary>
        public string Value { get; set; }
    }
}
