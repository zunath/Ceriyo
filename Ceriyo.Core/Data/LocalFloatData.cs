using Ceriyo.Core.Validation;
using Ceriyo.Core.Validation.Data;
using FluentValidation;

namespace Ceriyo.Core.Data
{
    public class LocalFloatData: BaseValidatable
    {
        public string Key { get; set; }
        public float Value { get; set; }

        public LocalFloatData()
        {
            
        }

        public LocalFloatData(string key, float value)
        {
            Key = key;
            Value = value;
        }

        private IValidator _validator;
        protected override IValidator Validator => _validator ?? (_validator = new LocalFloatDataValidator());
    }
}
