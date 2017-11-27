using System;
using Ceriyo.Core.Attributes;
using Ceriyo.Core.Data.Contracts;

namespace Ceriyo.Core.Data
{
    /// <summary>
    /// Stores local string data
    /// </summary>
    public class LocalStringData: IDataDomainObject
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
        /// The stored string value
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Constructs a new local string data object
        /// </summary>
        public LocalStringData()
        {
            Key = string.Empty;
            Value = string.Empty;
            GlobalID = Guid.NewGuid().ToString();
        }
        
        /// <summary>
        /// Constructs a new local string data object.
        /// </summary>
        /// <param name="key">The unique key</param>
        /// <param name="value">The stored string value</param>
        public LocalStringData(string key, string value)
        {
            Key = key;
            Value = value;
            GlobalID = Guid.NewGuid().ToString();
        }
    }
}
