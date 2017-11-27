using System.Collections.Generic;

namespace Ceriyo.Core.Scripting
{
    /// <summary>
    /// Details about a particular script methods which is used in the scripting engine.
    /// </summary>
    public class ScriptMethod
    {
        /// <summary>
        /// Name of the script method.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// A summary of the script method.
        /// </summary>
        public string Summary { get; set; }

        /// <summary>
        /// The details regarding what the method returns.
        /// </summary>
        public string Returns { get; set; }

        /// <summary>
        /// Parameters used by the script method.
        /// </summary>
        public List<string> Parameters { get; set; }

        /// <summary>
        /// The prototype of the script method.
        /// </summary>
        public string Prototype { get; set; }

        /// <inheritdoc />
        public ScriptMethod()
        {
            Parameters = new List<string>();
        }

    }
}
