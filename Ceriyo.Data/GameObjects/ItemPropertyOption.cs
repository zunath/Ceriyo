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
        public string Resref { get; set; }
        public BindingList<ItemPropertyOptionValue> Values { get; set; }
        public string ParentItemPropertyResref { get; set; }
        public bool AllowMultiple { get; set; }

        public ItemPropertyOption()
        {
            Name = string.Empty;
            Resref = string.Empty;
            Values = new BindingList<ItemPropertyOptionValue>();
            ParentItemPropertyResref = string.Empty;
            AllowMultiple = true;
        }
    }
}
