using FluentValidation;

namespace Ceriyo.Toolset.WPF.Views.NewAreaView
{
    public class NewAreaViewModelValidator: AbstractValidator<NewAreaViewModel>
    {
        public NewAreaViewModelValidator()
        {
            RuleFor(x => x.Name)
                .NotNull()
                .Length(1, 256);

            RuleFor(x => x.Tag)
                .NotNull()
                .Length(1, 64);

            RuleFor(x => x.Resref)
                .NotNull()
                .Length(1, 32);

            RuleFor(x => x.SelectedTileset)
                .NotNull();

            RuleFor(x => x.Width)
                .InclusiveBetween(1, 32);

            RuleFor(x => x.Height)
                .InclusiveBetween(1, 32);


        }
    }
}
