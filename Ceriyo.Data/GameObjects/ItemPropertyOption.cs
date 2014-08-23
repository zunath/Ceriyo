using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Ceriyo.Data.Extensions;

namespace Ceriyo.Data.GameObjects
{
    public class ItemPropertyOption
    {
        public string Name { get; set; }
        public string SelectedValue { get; set; }
        public SerializableDictionary<string, string> Values { get; set; }

        public ItemPropertyOption()
        {
            Name = string.Empty;
            SelectedValue = string.Empty;
            Values = new SerializableDictionary<string, string>();
        }
    }
}
