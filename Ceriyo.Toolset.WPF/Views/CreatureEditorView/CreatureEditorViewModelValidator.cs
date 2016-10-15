using Ceriyo.Core.Validation;
using Ceriyo.Core.Validation.Data;
using FluentValidation;

namespace Ceriyo.Toolset.WPF.Views.CreatureEditorView
{
    public class CreatureEditorViewModelValidator: AbstractValidator<CreatureEditorViewModel>
    {
        public CreatureEditorViewModelValidator()
        {
            RuleForEach(x => x.Creatures)
                .SetValidator(new CreatureDataValidator());

            RuleFor(x => x.Creatures)
                .Must((x) => !ValidationHelper.IsDuplicate(x, "Tag", "Tag must be unique."))
                .WithMessage("Tag must be unique.");

            RuleFor(x => x.Creatures)
                .Must((x) => !ValidationHelper.IsDuplicate(x, "Resref", "Resref must be unique."))
                .WithMessage("Resref must be unique.");
        }
    }
}
