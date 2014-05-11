using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using Ceriyo.Data;

namespace Ceriyo.Toolset.ValidationRules
{
    public class TextLengthVR : ValidationRule
    {
        public int MinimumLength { get; set; }
        public int MaximumLength { get; set; }
        public string ErrorMessage { get; set; }

        public TextLengthVR()
        {
            this.MinimumLength = 1;
            this.MaximumLength = 1;
        }

        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            ValidationResult result = new ValidationResult(true, null);
            string input = (value ?? string.Empty).ToString();
            if (input.Length < this.MinimumLength ||
               input.Length > this.MaximumLength)
            {
                result = new ValidationResult(false, this.ErrorMessage);
            }

            return result;
        }
    }
}
