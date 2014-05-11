using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace Ceriyo.Toolset.ValidationRules
{
    public class NumericAmountRule : ValidationRule
    {
        public int Minimum { get; set; }
        public int Maximum { get; set; }
        public string ErrorMessage { get; set; }

        public NumericAmountRule()
        {
            this.Minimum = 0;
            this.Maximum = 0;
        }

        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            ValidationResult result = new ValidationResult(false, null);
            if (value != null)
            {
                string inputText = Convert.ToString(value);
                int inputValue;
                if (int.TryParse(inputText, out inputValue))
                {
                    if (inputValue >= Minimum && inputValue <= Maximum)
                    {
                        result = new ValidationResult(true, null);
                    }
                }
            }

            return result;
        }
    }
}
