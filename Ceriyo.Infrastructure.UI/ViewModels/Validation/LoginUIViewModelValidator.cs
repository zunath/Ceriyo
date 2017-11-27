using FluentValidation;

namespace Ceriyo.Infrastructure.UI.ViewModels.Validation
{
    public class LoginUIViewModelValidator: AbstractValidator<LoginUIViewModel>
    {
        public LoginUIViewModelValidator()
        {
            RuleFor(x => x.Username)
                .NotEmpty()
                .NotNull();

            RuleFor(x => x.Password)
                .NotEmpty()
                .NotNull();
        }
    }
}
