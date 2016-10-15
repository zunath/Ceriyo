using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Ceriyo.Core.Attributes;
using Ceriyo.Core.Properties;
using FluentValidation;
using FluentValidation.Internal;

namespace Ceriyo.Core.Validation
{
    public abstract class BaseValidatable : INotifyPropertyChanged, INotifyDataErrorInfo
    {
        [SerializationIgnore]
        protected abstract IValidator Validator { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (!string.IsNullOrWhiteSpace(propertyName))
            {
                GetErrors(propertyName);
            }
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        
        public IEnumerable GetErrors(string propertyName)
        {
            if (string.IsNullOrWhiteSpace(propertyName)) return new List<string>();

            var context = new ValidationContext(this, new PropertyChain(),
                new MemberNameValidatorSelector(new[] { propertyName }));
            var result = Validator.Validate(context);
            if (Errors.ContainsKey(propertyName))
            {
                Errors.Remove(propertyName);
            }

            List<string> errors = new List<string>();
            errors.AddRange(result.Errors.Select(error => error.ErrorMessage));
            Errors[propertyName] = errors;

            var returnErrors = Errors[propertyName].ToList();
            if(ExternalErrors.ContainsKey(propertyName))
                returnErrors.Add(ExternalErrors[propertyName]);
            return returnErrors;
        }

        private Dictionary<string, string> _externalErrors;
        private Dictionary<string, string> ExternalErrors => _externalErrors ?? (_externalErrors = new Dictionary<string, string>());

        public void SetExternalError(string propertyName, string error)
        {
            if (string.IsNullOrWhiteSpace(propertyName) ||
                string.IsNullOrWhiteSpace(error)) return;
            ExternalErrors[propertyName] = error;
            RaiseErrorsChanged(propertyName);
        }

        public void ClearExternalError(string propertyName)
        {
            if (string.IsNullOrWhiteSpace(propertyName) ||
                !ExternalErrors.ContainsKey(propertyName)) return;
            ExternalErrors.Remove(propertyName);
            RaiseErrorsChanged(propertyName);
        }

        private Dictionary<string, List<string>> _errors;

        private Dictionary<string, List<string>> Errors => _errors ?? (_errors = new Dictionary<string, List<string>>());

        [SerializationIgnore]
        public bool HasErrors => Errors.Count > 0 || ExternalErrors.Count > 0;

        private void RaiseErrorsChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;
    }
}
