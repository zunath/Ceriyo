using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Ceriyo.Core.Attributes;
using Ceriyo.Core.Properties;
using FluentValidation;
using FluentValidation.Internal;

namespace Ceriyo.Core.Data
{
    public abstract class BaseDataRecord : IDataErrorInfo, INotifyPropertyChanged
    {
        [SerializationIgnore]
        public Contracts.IValidatorFactory ValidatorFactory { get; set; }
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
                if (ValidatorFactory == null) return string.Empty;

                if (string.IsNullOrWhiteSpace(columnName))
                {
                    var context = new ValidationContext(this);
                    var validator = ValidatorFactory.GetValidator(GetType());
                    var result = validator.Validate(context);
                    return string.Join(Environment.NewLine, result.Errors);
                }
                else
                {
                    var context = new ValidationContext(this, new PropertyChain(),
                        new MemberNameValidatorSelector(new[] { columnName }));

                    var validator = ValidatorFactory.GetValidator(GetType());
                    var result = validator.Validate(context);

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
