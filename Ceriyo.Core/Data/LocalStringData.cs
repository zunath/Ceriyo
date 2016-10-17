using System;

namespace Ceriyo.Core.Data
{
    public class LocalStringData
    {
        public string Key { get; set; }

        public string Value { get; set; }

        public LocalStringData()
        {
            Key = string.Empty;
            Value = string.Empty;
        }
        
        public LocalStringData(string key, string value)
        {
            Key = key;
            Value = value;
        }
    }
}
