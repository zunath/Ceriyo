using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Ceriyo.Core.Attributes;
using Ceriyo.Core.Properties;
using FluentValidation;
using FluentValidation.Internal;

namespace Ceriyo.Core.Validation
{
    public abstract class BaseValidatable : IDataErrorInfo, INotifyPropertyChanged
    {   
        [SerializationIgnore]
        protected abstract IValidator Validator { get; }

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
                if (string.IsNullOrWhiteSpace(columnName))
                {
                    var context = new ValidationContext(this);
                    var result = Validator.Validate(context);
                    return string.Join(Environment.NewLine, result.Errors);
                }
                else
                {
                    var context = new ValidationContext(this, new PropertyChain(),
                        new MemberNameValidatorSelector(new[] { columnName }));
                    var result = Validator.Validate(context);

                    return result.Errors.Any() ?
                        result.Errors.First().ErrorMessage :
                        string.Empty;
                }

            }
        }

        [SerializationIgnore]
        public string Error => this[null];

        [SerializationIgnore]
        public bool IsValid => string.IsNullOrWhiteSpace(Error);
    }
}
