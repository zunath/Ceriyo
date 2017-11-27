using System.Collections.Generic;
using Artemis.Interface;

namespace Ceriyo.Core.Components
{
    /// <summary>
    /// Tracks local data stored on entities via scripting.
    /// </summary>
    public class LocalData: IComponent
    {
        /// <summary>
        /// The local strings stored on this entity.
        /// </summary>
        public Dictionary<string, string> LocalStrings { get; set; }
        /// <summary>
        /// The local doubles stored on this entity.
        /// </summary>
        public Dictionary<string, double> LocalDoubles { get; set; }

        /// <summary>
        /// Constructs a LocalData component
        /// </summary>
        public LocalData()
        {
            LocalStrings = new Dictionary<string, string>();
            LocalDoubles = new Dictionary<string, double>();
        }
    }
}
