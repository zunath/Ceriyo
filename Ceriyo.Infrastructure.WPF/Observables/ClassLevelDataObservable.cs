using Ceriyo.Core.Contracts;
using Ceriyo.Core.Data;
using Ceriyo.Infrastructure.WPF.BindableBases;
using Ceriyo.Infrastructure.WPF.Validation.Validators;

namespace Ceriyo.Infrastructure.WPF.Observables
{
    public class ClassLevelDataObservable: ValidatableBindableBase<ClassLevelData>
    {
        public delegate ClassLevelDataObservable Factory(ClassLevelData data = null);

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
            
        }
        public ClassLevelDataObservable(ClassLevelDataObservableValidator validator,
            IObjectMapper objectMapper,
            ClassLevelData data = null)
            :base(objectMapper, validator, data)
        {
            if (data != null) return;
            Level = 0;
            ExperienceRequired = 0;
        }
    }
}
