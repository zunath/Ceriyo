﻿using Xceed.Wpf.Toolkit;

namespace Ceriyo.Infrastructure.WPF.Controls
{
    public class IntegerUpDownEx: IntegerUpDown
    {
        protected override void OnTextChanged(string oldValue, string newValue)
        {
            base.OnTextChanged(oldValue, newValue);

            if (string.IsNullOrWhiteSpace(oldValue)) return;

            int val;
            if (!int.TryParse(newValue, out val))
            {
                Text = oldValue;
            }

        }
    }
}
