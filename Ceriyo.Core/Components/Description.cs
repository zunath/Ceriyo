using Artemis.Interface;

namespace Ceriyo.Core.Components
{
    public class Description: IComponent
    {
        /// <summary>
        /// The actual description.
        /// </summary>
        public string Value { get; set; }

        public Description()
        {
            Value = string.Empty;
        }
    }
}
