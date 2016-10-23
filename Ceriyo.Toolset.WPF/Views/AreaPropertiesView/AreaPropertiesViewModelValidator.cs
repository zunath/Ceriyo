using Ceriyo.Infrastructure.WPF.Validation.Validators;
using FluentValidation;

namespace Ceriyo.Toolset.WPF.Views.AreaPropertiesView
{
    public class AreaPropertiesViewModelValidator: AbstractValidator<AreaPropertiesViewModel>
    {
        public AreaPropertiesViewModelValidator()
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

            RuleFor(x => x.Description)
                .Length(0, 2000);

            RuleFor(x => x.Comments)
                .Length(0, 5000);

            RuleFor(x => x.SelectedTileset)
                .NotNull();

            RuleFor(x => x.Width)
                .InclusiveBetween(1, 32);

            RuleFor(x => x.Height)
                .InclusiveBetween(1, 32);

            RuleFor(x => x.LocalVariables)
                .NotNull()
                .SetValidator(new LocalVariableDataObservableValidator());


        }
    }
}
