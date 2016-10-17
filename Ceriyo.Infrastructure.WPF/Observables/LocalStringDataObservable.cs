using Ceriyo.Core.Contracts;
using Ceriyo.Core.Data;
using Ceriyo.Infrastructure.WPF.BindableBases;
using Ceriyo.Infrastructure.WPF.Validation.Validators;

namespace Ceriyo.Infrastructure.WPF.Observables
{
    public class LocalStringDataObservable: ValidatableBindableBase<LocalStringData>
    {
        public delegate LocalStringDataObservable Factory(LocalStringData data = null);

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

        public LocalStringDataObservable(LocalStringDataObservableValidator validator,
            IObjectMapper objectMapper,
            LocalStringData data = null)
            :base(objectMapper, validator, data)
        {
        }
    }
}
