using Ceriyo.Infrastructure.WPF.Validation;
using Ceriyo.Infrastructure.WPF.Validation.Validators;
using FluentValidation;

namespace Ceriyo.Toolset.WPF.Views.AbilityEditorView
{
    public class AbilityEditorViewModelValidator: AbstractValidator<AbilityEditorViewModel>
    {
        public AbilityEditorViewModelValidator()
        {
            RuleForEach(x => x.Abilities)
                .SetValidator(new AbilityDataObservableValidator());

            //RuleFor(x => x.Abilities)
            //    .Must((x) => !ValidationHelper.IsDuplicate(x, "Tag", "Tag must be unique."))
            //    .WithMessage("Tag must be unique.");

            //RuleFor(x => x.Abilities)
            //    .Must((x) => !ValidationHelper.IsDuplicate(x, "Resref", "Resref must be unique."))
            //    .WithMessage("Resref must be unique.");
        }
    }
}
