using Ceriyo.Infrastructure.WPF.Observables;
using FluentValidation;

namespace Ceriyo.Infrastructure.WPF.Validation.Validators
{
    public class ScriptDataObservableValidator: AbstractValidator<ScriptDataObservable>
    {
        public ScriptDataObservableValidator()
        {
            RuleFor(x => x.GlobalID)
                .NotNull()
                .NotEmpty();

            RuleFor(x => x.Name)
                .NotEmpty()
                .Length(1, 256);

            RuleFor(x => x.Resref)
                .NotEmpty()
                .Length(1, 32);
        }
    }
}
