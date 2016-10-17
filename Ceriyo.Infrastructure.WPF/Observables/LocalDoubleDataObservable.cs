using Ceriyo.Core.Contracts;
using Ceriyo.Core.Data;
using Ceriyo.Infrastructure.WPF.BindableBases;
using Ceriyo.Infrastructure.WPF.Validation.Validators;

namespace Ceriyo.Infrastructure.WPF.Observables
{
    public class LocalDoubleDataObservable: ValidatableBindableBase<LocalDoubleData>
    {
        public delegate LocalDoubleDataObservable Factory(LocalDoubleData data = null);
        
        private string _key;
        private double _value;
        
        public string Key
        {
            get { return _key; }
            set { SetProperty(ref _key, value); }
        }

        public double Value
        {
            get { return _value; }
            set { SetProperty(ref _value, value); }
        }

        public LocalDoubleDataObservable(LocalDoubleDataObservableValidator validator,
            IObjectMapper objectMapper,
            LocalDoubleData data = null)
            :base(objectMapper, validator, data)
        {
            _key = string.Empty;
            _value = 0.0;
        }
    }
}
