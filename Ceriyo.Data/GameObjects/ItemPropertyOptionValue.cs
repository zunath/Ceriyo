using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ceriyo.Data.Extensions;

namespace Ceriyo.Data.GameObjects
{
    public class ItemPropertyOptionValue
    {
        public string ParentOptionResref { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }

        public ItemPropertyOptionValue(string parentResref, string key, string value)
        {
            this.ParentOptionResref = parentResref;
            this.Key = key;
            this.Value = value;
        }

        public ItemPropertyOptionValue()
        {
            this.ParentOptionResref = string.Empty;
            this.Key = string.Empty;
            this.Value = string.Empty;
        }
    }
}
