using Ceriyo.Infrastructure.WPF.Validation.Validators;
using FluentValidation;

namespace Ceriyo.Toolset.WPF.Views.SkillEditorView
{
    public class SkillEditorViewModelValidator: AbstractValidator<SkillEditorViewModel>
    {
        public SkillEditorViewModelValidator()
        {
            RuleForEach(x => x.Skills)
                .SetValidator(new SkillDataObservableValidator());

            //RuleFor(x => x.Skills)
            //    .Must((x) => !ValidationHelper.IsDuplicate(x, "Tag", "Tag must be unique."))
            //    .WithMessage("Tag must be unique.");

            //RuleFor(x => x.Skills)
            //    .Must((x) => !ValidationHelper.IsDuplicate(x, "Resref", "Resref must be unique."))
            //    .WithMessage("Resref must be unique.");
        }
    }
}
