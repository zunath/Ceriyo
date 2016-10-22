using Ceriyo.Infrastructure.WPF.BindableBases;
using Ceriyo.Infrastructure.WPF.Observables.Contracts;
using Ceriyo.Infrastructure.WPF.Validation.Validators;

namespace Ceriyo.Infrastructure.WPF.Observables
{
    public class LocalStringDataObservable: ValidatableBindableBase<LocalStringDataObservableValidator>, IDataObservable
    {
        private string _key;
        private string _value;

        public string Key
        {
            get { return _key; }
            set { SetProperty(ref _key, value); }
        }

        public string Value
        {
            get { return _value; }
            set { SetProperty(ref _value, value); }
        }
        
        public LocalStringDataObservable()
        {
            Key = string.Empty;
            Value = string.Empty;
        }
    }
}
