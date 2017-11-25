using System;

namespace Ceriyo.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ScriptNamespaceAttribute : Attribute
    {
        public string Namespace { get; set; }

        public ScriptNamespaceAttribute(string @namespace)
        {
            Namespace = @namespace;
        }
    }
}
