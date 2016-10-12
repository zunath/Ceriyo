using System.Linq;
using FluentValidation;

namespace Ceriyo.Toolset.WPF.Views.ModulePropertiesView
{
    public class ModulePropertiesViewModelValidator: AbstractValidator<ModulePropertiesViewModel>
    {
        public ModulePropertiesViewModelValidator()
        {
            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty()
                .Length(1, 256);

            RuleFor(x => x.Tag)
                .NotNull()
                .NotEmpty()
                .Length(1, 64);

            RuleFor(x => x.Resref)
                .NotNull()
                .NotEmpty()
                .Length(1, 32);

            RuleFor(x => x.MaxLevel)
                .NotNull()
                .NotEmpty()
                .InclusiveBetween(1, 99);

            RuleFor(x => x.Description)
                .Length(0, 2000);

            RuleFor(x => x.Comments)
                .Length(0, 5000);

            //RuleForEach(x => x.LocalStrings)
            //    .Must(x => !string.IsNullOrWhiteSpace(x.Key))
            //    .WithMessage("Key must not be blank.");

            //RuleFor(x => x.LocalStrings)
            //    .Must(x => x.SelectMany(y => y.Key).Count() == x.SelectMany(y => y.Key).Distinct().Count())
            //    .WithMessage("Keys must be unique.");

            //RuleFor(x => x.LocalFloats)
            //    .Must(x => x.SelectMany(y => y.Key).Count() == x.SelectMany(y => y.Key).Distinct().Count())
            //    .WithMessage("Keys must be unique.");


        }
    }
}
