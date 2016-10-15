using Ceriyo.Core.Validation;
using Ceriyo.Core.Validation.Data;
using FluentValidation;

namespace Ceriyo.Core.Data
{
    public class LocalStringData: BaseValidatable
    {
        private string _key;
        private string _value;

        public string Key
        {
            get { return _key; }
            set
            {
                _key = value;
                OnPropertyChanged();
            }
        }

        public string Value
        {
            get { return _value; }
            set
            {
                _value = value;
                OnPropertyChanged();
            }
        }

        public LocalStringData()
        {
            Key = string.Empty;
            Value = string.Empty;
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
