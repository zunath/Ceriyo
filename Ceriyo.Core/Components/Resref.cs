using Artemis.Interface;

namespace Ceriyo.Core.Components
{
    /// <summary>
    /// Tracks the resource reference of an entity.
    /// </summary>
    public class Resref: IComponent
    {
        /// <summary>
        /// The resource reference (resref)
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Constructs a new resref component.
        /// </summary>
        public Resref()
        {
            Value = string.Empty;
        }
    }
}
