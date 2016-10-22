using Ceriyo.Infrastructure.WPF.BindableBases;
using Ceriyo.Infrastructure.WPF.Observables.Contracts;
using Ceriyo.Infrastructure.WPF.Validation.Validators;

namespace Ceriyo.Infrastructure.WPF.Observables
{
    public class ClassLevelDataObservable: ValidatableBindableBase<ClassLevelDataObservableValidator>, IDataObservable
    {
        private int _level;
        private int _experienceRequired;

        public int Level
        {
            get { return _level; }
            set { SetProperty(ref _level, value); }
        }

        public int ExperienceRequired
        {
            get { return _experienceRequired; }
            set { SetProperty(ref _experienceRequired, value); }
        }
        
        public ClassLevelDataObservable()
        {
            Level = 0;
            ExperienceRequired = 0;
        }
    }
}
