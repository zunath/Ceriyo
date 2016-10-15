using Ceriyo.Core.Validation;
using Ceriyo.Core.Validation.Data;
using FluentValidation;

namespace Ceriyo.Toolset.WPF.Views.SkillEditorView
{
    public class SkillEditorViewModelValidator: AbstractValidator<SkillEditorViewModel>
    {
        public SkillEditorViewModelValidator()
        {
            RuleForEach(x => x.Skills)
                .SetValidator(new SkillDataValidator());

            RuleFor(x => x.Skills)
                .Must((x) => !ValidationHelper.IsDuplicate(x, "Tag", "Tag must be unique."))
                .WithMessage("Tag must be unique.");

            RuleFor(x => x.Skills)
                .Must((x) => !ValidationHelper.IsDuplicate(x, "Resref", "Resref must be unique."))
                .WithMessage("Resref must be unique.");
        }
    }
}
