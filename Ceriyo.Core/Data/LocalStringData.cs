using Ceriyo.Core.Validation;
using Ceriyo.Core.Validation.Data;
using FluentValidation;

namespace Ceriyo.Core.Data
{
    public class LocalStringData: BaseValidatable
    {
        public string Key { get; set; }
        public string Value { get; set; }

        public LocalStringData()
        {
            
        }

        public LocalStringData(string key, string value)
        {
            Key = key;
            Value = value;
        }

        private IValidator _validator;
        protected override IValidator Validator => _validator ?? (_validator = new LocalStringDataValidator());
    }
}
