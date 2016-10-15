using System;
using System.Collections;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Ceriyo.Core.Attributes;
using FluentValidation;
using FluentValidation.Internal;
using Prism.Mvvm;

namespace Ceriyo.Infrastructure.WPF.BindableBases
{
    public abstract class ValidatableBindableBase: BindableBase, INotifyDataErrorInfo
    {
        protected override bool SetProperty<T>(ref T storage, T value, [CallerMemberName]string propertyName = null)
        {
            var result = base.SetProperty(ref storage, value, propertyName);
            if (result && !string.IsNullOrWhiteSpace(propertyName))
            {
                ValidateProperty(propertyName);
            }

            return result;
        }

        private void ValidateProperty(string propertyName)
        {
            var context = new ValidationContext(this, new PropertyChain(),
                new MemberNameValidatorSelector(new[] { propertyName }));
            var result = Validator.Validate(context);
            string[] errors = result.Errors.Select(error => error.ErrorMessage).ToArray();
            ErrorsContainer.SetErrors(propertyName, errors);
        }

        protected abstract IValidator Validator { get; }
        
        public IEnumerable GetErrors(string propertyName)
        {
            return ErrorsContainer.GetErrors(propertyName);
        }

        [SerializationIgnore]
        public bool HasErrors => !Validator.Validate(this).IsValid;

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged = delegate { };


        private ErrorsContainer<string> _errorsContainer;

        protected ErrorsContainer<string> ErrorsContainer => _errorsContainer ?? (_errorsContainer = new ErrorsContainer<string>(RaiseErrorsChanged));

        private void RaiseErrorsChanged(string propertyName)
        {
            OnErrorsChanged(new DataErrorsChangedEventArgs(propertyName));
        }

        protected virtual void OnErrorsChanged(DataErrorsChangedEventArgs e)
        {
            ErrorsChanged(this, e);
        }
    }
}
