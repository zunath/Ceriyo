﻿using Ceriyo.Core.Data.Contracts;

namespace Ceriyo.Core.Data
{
    public class LocalDoubleData : IDataDomainObject
    {
        public string Key { get; set; }

        public double Value { get; set; }

        public LocalDoubleData()
        {
            Key = string.Empty;
            Value = 0.0f;
        }

        public LocalDoubleData(string key, double value)
        {
            Key = key;
            Value = value;
        }
    }
}
