using Ceriyo.Infrastructure.WPF.Observables;
using FluentValidation;

namespace Ceriyo.Infrastructure.WPF.Validation.Validators
{
    public class ClassRequirementDataObservableValidator: AbstractValidator<ClassRequirementDataObservable>
    {
        public ClassRequirementDataObservableValidator()
        {
            RuleFor(x => x.ClassResref)
                .NotEmpty()
                .Length(1, 32);

            RuleFor(x => x.LevelRequired)
                .InclusiveBetween(1, 99);
        }
    }
}
