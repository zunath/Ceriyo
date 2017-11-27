using System;

namespace Ceriyo.Core.Attributes
{
    /// <summary>
    /// Identifies which namespace a group of script methods will reside.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class ScriptNamespaceAttribute : Attribute
    {
        /// <summary>
        /// Name of the namespace
        /// </summary>
        public string Namespace { get; set; }

        /// <summary>
        /// Constructs a ScriptNamespaceAttribute
        /// </summary>
        /// <param name="namespace">Name of the namespace</param>
        public ScriptNamespaceAttribute(string @namespace)
        {
            Namespace = @namespace;
        }
    }
}
