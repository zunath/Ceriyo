using Ceriyo.Infrastructure.WPF.Observables;
using FluentValidation;

namespace Ceriyo.Infrastructure.WPF.Validation.Validators
{
    public class LocalDoubleDataObservableValidator: AbstractValidator<LocalDoubleDataObservable>
    {
        public LocalDoubleDataObservableValidator()
        {
            RuleFor(x => x.Key)
                .NotNull()
                .NotEmpty();
        }
    }
}
