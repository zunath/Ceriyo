using Ceriyo.Infrastructure.WPF.Observables;
using FluentValidation;

namespace Ceriyo.Infrastructure.WPF.Validation.Validators
{
    public class CreatureDataObservableValidator: AbstractValidator<CreatureDataObservable>
    {
        public CreatureDataObservableValidator(LocalVariableDataObservableValidator localValidator)
        {
            RuleFor(x => x.GlobalID)
                .NotNull()
                .NotEmpty();

            RuleFor(x => x.Name)
                .NotEmpty()
                .Length(1, 256);

            RuleFor(x => x.Tag)
                .NotEmpty()
                .Length(1, 64);

            RuleFor(x => x.Resref)
                .NotEmpty()
                .Length(1, 32);

            RuleFor(x => x.Comment)
                .Length(0, 5000);

            RuleFor(x => x.Description)
                .Length(0, 2000);

            RuleFor(x => x.Level)
                .InclusiveBetween(1, 99);

            RuleFor(x => x.ClassResref)
                .NotEmpty()
                .Length(1, 32)
                .WithMessage("A class must be selected.");

            RuleFor(x => x.LocalVariables)
                .NotNull()
                .SetValidator(localValidator);

        }
    }
}
