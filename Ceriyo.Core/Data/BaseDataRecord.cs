using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Ceriyo.Core.Attributes;
using Ceriyo.Core.Properties;
using FluentValidation;
using FluentValidation.Internal;

namespace Ceriyo.Core.Data
{
    public abstract class BaseDataRecord: IDataErrorInfo, INotifyPropertyChanged
    {
        [SerializationIgnore]
        public Contracts.IValidatorFactory ValidatorFactory { get; set; }

        protected BaseDataRecord()
        {
            // This is pretty ridiculous but WPF doesn't evaluate error binding on start up
            // unless the property changes. This is a hack to get around this issue.
            // We simply take the every property's current value and set it to the same value.
            string[] ignoreableProperties = { "OnPropertyChanged", "Item", "Error", "IsValid" };

            foreach (var property in GetType().GetProperties())
            {
                if (ignoreableProperties.Contains(property.Name)) continue;
                object value = property.GetValue(this);
                property.SetValue(this, value);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        [SerializationIgnore]
        public string this[string columnName]
        {
            get
            {
                var context = new ValidationContext(this, new PropertyChain(), 
                    new MemberNameValidatorSelector(new []{columnName}));

                var validator = ValidatorFactory.GetValidator(GetType());
                var result = validator.Validate(context);

                return result.Errors.Any() ?
                    result.Errors.First().ErrorMessage :
                    string.Empty;
            }
        }

        private string _error;

        [SerializationIgnore]
        public string Error
        {
            get { return _error; }
            set
            {
                _error = value;
                OnPropertyChanged();
            }
        }

        [SerializationIgnore]
        public bool IsValid
        {
            get
            {
                bool isValid = true;
                foreach (var propertyInfo in GetType().GetProperties())
                {
                    string propertyName = propertyInfo.Name;
                    if (!string.IsNullOrWhiteSpace(this[propertyName]))
                    {
                        isValid = false;
                        break;
                    }
                }
                
                
                return isValid;
            }
        }
        
    }
}
