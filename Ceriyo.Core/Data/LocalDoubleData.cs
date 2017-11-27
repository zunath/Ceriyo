using System;
using Ceriyo.Core.Attributes;
using Ceriyo.Core.Data.Contracts;

namespace Ceriyo.Core.Data
{
    /// <summary>
    /// Stores local double data.
    /// </summary>
    public class LocalDoubleData: IDataDomainObject
    {
        /// <inheritdoc />
        public string GlobalID { get; set; }

        /// <inheritdoc />
        [SerializationIgnore]
        public string DirectoryName => null;

        /// <summary>
        /// The unique key 
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// The value stored
        /// </summary>
        public double Value { get; set; }

        /// <summary>
        /// Constructs a new local double data object.
        /// </summary>
        public LocalDoubleData()
        {
            Key = string.Empty;
            Value = 0.0f;
            GlobalID = Guid.NewGuid().ToString();
        }

        /// <summary>
        /// Constructs a new local double data object.
        /// </summary>
        /// <param name="key">The key of the data.</param>
        /// <param name="value">The value stored.</param>
        public LocalDoubleData(string key, double value)
        {
            Key = key;
            Value = value;
            GlobalID = Guid.NewGuid().ToString();
        }
    }
}
