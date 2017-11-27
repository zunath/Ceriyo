using Artemis.Interface;

namespace Ceriyo.Core.Components
{
    /// <summary>
    /// Tracks the description on an entity
    /// </summary>
    public class Description: IComponent
    {
        /// <summary>
        /// The actual description.
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Constructs a description component
        /// </summary>
        public Description()
        {
            Value = string.Empty;
        }
    }
}
