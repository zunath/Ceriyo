using Ceriyo.Core.Validation;
using Ceriyo.Core.Validation.Data;
using FluentValidation;

namespace Ceriyo.Core.Data
{
    public class LocalFloatData: BaseValidatable
    {
        private string _key;
        private float _value;

        public string Key
        {
            get { return _key; }
            set
            {
                _key = value;
                OnPropertyChanged();
            }
        }

        public float Value
        {
            get { return _value; }
            set
            {
                _value = value;
                OnPropertyChanged();
            }
        }

        public LocalFloatData()
        {
            Key = string.Empty;
            Value = 0.0f;
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
