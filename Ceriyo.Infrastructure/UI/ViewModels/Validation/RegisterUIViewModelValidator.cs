using FluentValidation;

namespace Ceriyo.Infrastructure.UI.ViewModels.Validation
{
    public class RegisterUIViewModelValidator: AbstractValidator<RegisterUIViewModel>
    {
        public RegisterUIViewModelValidator()
        {
            RuleFor(x => x.Username)
                .NotEmpty()
                .NotNull()
                .MinimumLength(4)
                .MaximumLength(256);

            RuleFor(x => x.Password)
                .NotEmpty()
                .NotNull()
                .MinimumLength(8)
                .MaximumLength(100);

            RuleFor(x => x.ConfirmPassword)
                .NotEmpty()
                .NotNull()
                .Matches(y => y.Password);

            RuleFor(x => x.Email)
                .NotEmpty()
                .NotNull()
                .EmailAddress();
        }
    }
}
