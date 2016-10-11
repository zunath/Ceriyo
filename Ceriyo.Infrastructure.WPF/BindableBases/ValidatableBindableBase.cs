using System;
using System.ComponentModel;
using System.Linq;
using Ceriyo.Core.Attributes;
using FluentValidation;
using FluentValidation.Internal;
using Prism.Mvvm;

namespace Ceriyo.Infrastructure.WPF.BindableBases
{
    public abstract class ValidatableBindableBase: BindableBase, IDataErrorInfo, INotifyPropertyChanged
    {
        protected abstract IValidator Validator { get; }
        
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
