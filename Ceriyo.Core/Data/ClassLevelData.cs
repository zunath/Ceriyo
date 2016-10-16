using Ceriyo.Core.Validation;
using Ceriyo.Core.Validation.Data;
using FluentValidation;

namespace Ceriyo.Core.Data
{
    public class ClassLevelData: BaseValidatable
    {
        private int _level;
        private int _experienceRequired;

        public int Level
        {
            get { return _level; }
            set
            {
                _level = value;
                OnPropertyChanged();
            }
        }

        public int ExperienceRequired
        {
            get { return _experienceRequired; }
            set
            {
                _experienceRequired = value;
                OnPropertyChanged();
            }
        }

        private IValidator _validator;
        protected override IValidator Validator => _validator ?? (_validator = new ClassLevelDataValidator());
    }
}
