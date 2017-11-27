using Artemis.Interface;

namespace Ceriyo.Core.Components
{
    /// <summary>
    /// Tracks the tag of an entity.
    /// </summary>
    public class Tag: IComponent
    {
        /// <summary>
        /// The actual tag value
        /// </summary>
        public string Value { get; set; }
        
        /// <summary>
        /// Constructs a new Tag component.
        /// </summary>
        public Tag()
        {
            Value = string.Empty;
        }
    }
}
