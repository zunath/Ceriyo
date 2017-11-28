using FluentValidation;

namespace Ceriyo.Infrastructure.UI.ViewModels.Validation
{
    public class AccountHelpUIViewModelValidator: AbstractValidator<AccountHelpUIViewModel>
    {
        public AccountHelpUIViewModelValidator()
        {
            RuleFor(x => x.Email)
                .NotNull()
                .NotEmpty()
                .EmailAddress();
        }
    }
}
