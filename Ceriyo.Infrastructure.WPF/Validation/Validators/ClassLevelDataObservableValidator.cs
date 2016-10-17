using Ceriyo.Infrastructure.WPF.Observables;
using FluentValidation;

namespace Ceriyo.Infrastructure.WPF.Validation.Validators
{
    public class ClassLevelDataObservableValidator: AbstractValidator<ClassLevelDataObservable>
    {
        public ClassLevelDataObservableValidator()
        {
            RuleFor(x => x.ExperienceRequired)
                .NotNull()
                .NotEmpty()
                .GreaterThanOrEqualTo(1);

            RuleFor(x => x.Level)
                .InclusiveBetween(1, 99);
        }
    }
}
