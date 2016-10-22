using Ceriyo.Infrastructure.WPF.BindableBases;
using Ceriyo.Infrastructure.WPF.Observables.Contracts;
using Ceriyo.Infrastructure.WPF.Validation.Validators;

namespace Ceriyo.Infrastructure.WPF.Observables
{
    public class ClassRequirementDataObservable: ValidatableBindableBase<ClassRequirementDataObservableValidator>, IDataObservable
    {
        private string _classResref;

        public string ClassResref
        {
            get { return _classResref; }
            set { SetProperty(ref _classResref, value); }
        }


        private int _levelRequired;

        public int LevelRequired
        {
            get { return _levelRequired; }
            set { SetProperty(ref _levelRequired, value); }
        }
        
        public ClassRequirementDataObservable()
        {
            ClassResref = string.Empty;
            LevelRequired = 0;
        }
    }
}
