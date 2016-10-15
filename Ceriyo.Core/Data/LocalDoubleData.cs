using Ceriyo.Core.Validation;
using Ceriyo.Core.Validation.Data;
using FluentValidation;

namespace Ceriyo.Core.Data
{
    public class LocalDoubleData: BaseValidatable
    {
        private string _key;
        private double _value;

        public string Key
        {
            get { return _key; }
            set
            {
                _key = value;
                OnPropertyChanged();
            }
        }

        public double Value
        {
            get { return _value; }
            set
            {
                _value = value;
                OnPropertyChanged();
            }
        }

        public LocalDoubleData()
        {
            Key = string.Empty;
            Value = 0.0f;
        }

        public LocalDoubleData(string key, double value)
        {
            Key = key;
            Value = value;
        }

        private IValidator _validator;
        protected override IValidator Validator => _validator ?? (_validator = new LocalFloatDataValidator());
    }
}
