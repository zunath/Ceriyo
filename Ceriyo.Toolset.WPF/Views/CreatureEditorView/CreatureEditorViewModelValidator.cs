using Ceriyo.Infrastructure.WPF.Validation.Validators;
using FluentValidation;

namespace Ceriyo.Toolset.WPF.Views.CreatureEditorView
{
    public class CreatureEditorViewModelValidator: AbstractValidator<CreatureEditorViewModel>
    {
        public CreatureEditorViewModelValidator()
        {
            RuleForEach(x => x.Creatures)
                .SetValidator(new CreatureDataObservableValidator());

            //RuleFor(x => x.Creatures)
            //    .Must((x) => !ValidationHelper.IsDuplicate(x, "Tag", "Tag must be unique."))
            //    .WithMessage("Tag must be unique.");

            //RuleFor(x => x.Creatures)
            //    .Must((x) => !ValidationHelper.IsDuplicate(x, "Resref", "Resref must be unique."))
            //    .WithMessage("Resref must be unique.");
        }
    }
}
