using Ceriyo.Core.Contracts;
using Ceriyo.Core.Validation;
using Ceriyo.Core.Validation.Data;
using FluentValidation;

namespace Ceriyo.Core.Data
{
    public class ClassRequirementData: BaseValidatable
    {
        private int _levelRequired;
        private string _classResref;

        public string ClassResref
        {
            get { return _classResref; }
            set
            {
                if (value == _classResref) return;
                _classResref = value;
                OnPropertyChanged();
            }
        }

        public int LevelRequired
        {
            get { return _levelRequired; }
            set
            {
                if (value == _levelRequired) return;
                _levelRequired = value;
                OnPropertyChanged();
            }
        }

        public ClassRequirementData()
        {
            
        }


        private IValidator _validator;
        protected override IValidator Validator => _validator ?? (_validator = new ClassRequirementDataValidator());
    }
}
