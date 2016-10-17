using Ceriyo.Infrastructure.WPF.Observables;
using Ceriyo.Infrastructure.WPF.Validation.Contracts;
using FluentValidation;

namespace Ceriyo.Infrastructure.WPF.Validation.Validators
{
    public class LocalVariableDataObservableValidator: AbstractValidator<LocalVariableDataObservable>
    {
        public LocalVariableDataObservableValidator(IValidationHelper validationHelper, 
            LocalStringDataObservableValidator localStringValidator,
            LocalDoubleDataObservableValidator localDoubleValidator)
        {
            RuleFor(x => x.GlobalID)
                .NotNull()
                .NotEmpty();

            //RuleFor(x => x.LocalStrings)
            //    .Must((x) => !validationHelper.IsDuplicate(x, "Key", "Keys must be unique."))
            //    .WithMessage("Keys must be unique.");

            //RuleFor(x => x.LocalDoubles)
            //    .Must((x) => !validationHelper.IsDuplicate(x, "Key", "Keys must be unique."))
            //    .WithMessage("Keys must be unique.");

            RuleForEach(x => x.LocalStrings)
                .SetValidator(localStringValidator);

            RuleForEach(x => x.LocalDoubles)
                .SetValidator(localDoubleValidator);
        }
    }
}
