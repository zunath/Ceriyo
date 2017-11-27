using System;

namespace Ceriyo.Core.Attributes
{
    /// <summary>
    /// Identifies that a property will be ignored during serialization/deserialization.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class SerializationIgnoreAttribute: Attribute
    {
    }
}
