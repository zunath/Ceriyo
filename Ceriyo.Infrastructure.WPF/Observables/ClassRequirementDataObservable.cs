using Ceriyo.Core.Contracts;
using Ceriyo.Core.Data;
using Ceriyo.Infrastructure.WPF.BindableBases;
using Ceriyo.Infrastructure.WPF.Validation.Validators;

namespace Ceriyo.Infrastructure.WPF.Observables
{
    public class ClassRequirementDataObservable: ValidatableBindableBase<ClassRequirementData>
    {
        public delegate ClassRequirementDataObservable Factory();
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
        
        public ClassRequirementDataObservable(ClassRequirementDataObservableValidator validator,
            IObjectMapper objectMapper)
            :base(objectMapper, validator)
        {

        }
    }
}
