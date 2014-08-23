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
        public string SelectedValue { get; set; }
        public BindingList<ItemPropertyOptionValue> Values { get; set; }
        public string ParentItemPropertyResref { get; set; }

        public ItemPropertyOption()
        {
            Name = string.Empty;
            Resref = string.Empty;
            SelectedValue = string.Empty;
            Values = new BindingList<ItemPropertyOptionValue>();
            ParentItemPropertyResref = string.Empty;
        }
    }
}
