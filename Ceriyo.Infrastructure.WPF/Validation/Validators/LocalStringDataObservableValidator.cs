using Ceriyo.Infrastructure.WPF.Observables;
using FluentValidation;

namespace Ceriyo.Infrastructure.WPF.Validation.Validators
{
    public class LocalStringDataObservableValidator: AbstractValidator<LocalStringDataObservable>
    {
        public LocalStringDataObservableValidator()
        {

            RuleFor(x => x.Key)
                .NotNull()
                .NotEmpty();

            RuleFor(x => x.Value)
                .NotNull();
        }
    }
}
