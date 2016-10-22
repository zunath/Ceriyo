using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Ceriyo.Core.Attributes;
using Ceriyo.Infrastructure.WPF.Validation;
using FluentValidation;
using Prism.Mvvm;

namespace Ceriyo.Infrastructure.WPF.BindableBases
{
    public abstract class ValidatableBindableBase<T>: BindableBase, INotifyDataErrorInfo
        where T: IValidator
    {
        private List<string> _propertyNames;
        private readonly IValidator _validator;

        protected ValidatableBindableBase()
        {
            _validator = (IValidator)Activator.CreateInstance(typeof(T));
            LoadPropertyNames();
            ErrorsContainer = new ErrorsContainer<string>(RaiseErrorsChanged);
            DoValidate();
        }

        private void LoadPropertyNames()
        {
            _propertyNames = new List<string>();
            foreach (var propertyInfo in GetType().GetProperties())
            {
                _propertyNames.Add(propertyInfo.Name);
            }
        }

        private void DoValidate(string propertyName = null)
        {
            if (string.IsNullOrWhiteSpace(propertyName))
            {
                foreach (var prop in _propertyNames)
                {
                    IEnumerable<string> errors = ValidateProperty(prop);
                    ErrorsContainer.SetErrors(prop, errors);
                }
            }
            else
            {
                IEnumerable<string> errors = ValidateProperty(propertyName);
                ErrorsContainer.SetErrors(propertyName, errors);
            }
        }

        protected override bool SetProperty<T>(ref T storage, T value, [CallerMemberName]string propertyName = null)
        {
            var result = base.SetProperty(ref storage, value, propertyName);
            if (result && !string.IsNullOrWhiteSpace(propertyName))
            {
                DoValidate(propertyName);
            }

            return result;
        }

        public void ValidateObject()
        {
            DoValidate();
        }

        public IEnumerable<string> ValidateProperty(string propertyName)
        {
            return _validator.ValidatePropertyAndReturnErrors(this, propertyName);
        }


        public IEnumerable GetErrors(string propertyName)
        {
            return ErrorsContainer.GetErrors(propertyName);
        }

        [SerializationIgnore]
        public bool HasErrors => ErrorsContainer.HasErrors;

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged = delegate { };


        private ErrorsContainer<string> ErrorsContainer { get; }

        private void RaiseErrorsChanged(string propertyName)
        {
            OnErrorsChanged(new DataErrorsChangedEventArgs(propertyName));
        }

        protected virtual void OnErrorsChanged(DataErrorsChangedEventArgs e)
        {
            ErrorsChanged(this, e);
        }

        public void SetExternalError(string propertyName, string error)
        {
            if (string.IsNullOrWhiteSpace(propertyName) ||
                string.IsNullOrWhiteSpace(error)) return;

            ErrorsContainer.SetErrors(propertyName, new []{error});
        }

        public void ClearExternalError(string propertyName)
        {
            // TODO: Implement
        }
    }
}
