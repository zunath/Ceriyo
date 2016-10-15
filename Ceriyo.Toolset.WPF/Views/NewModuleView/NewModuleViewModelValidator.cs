using FluentValidation;

namespace Ceriyo.Toolset.WPF.Views.NewModuleView
{
    public class NewModuleViewModelValidator: AbstractValidator<NewModuleViewModel>
    {
        public NewModuleViewModelValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .Length(1, 256);

            RuleFor(x => x.Tag)
                .NotEmpty()
                .Length(1, 64);

            RuleFor(x => x.Resref)
                .NotEmpty()
                .Length(1, 32);
        }
    }
}
