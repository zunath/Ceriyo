using Ceriyo.Infrastructure.WPF.BindableBases;
using Ceriyo.Infrastructure.WPF.Observables.Contracts;
using Ceriyo.Infrastructure.WPF.Validation.Validators;

namespace Ceriyo.Infrastructure.WPF.Observables
{
    public class LocalDoubleDataObservable: ValidatableBindableBase<LocalDoubleDataObservableValidator>, IDataObservable
    {
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
        
        public LocalDoubleDataObservable()
        {
            Key = string.Empty;
            Value = 0.0;
        }
    }
}
