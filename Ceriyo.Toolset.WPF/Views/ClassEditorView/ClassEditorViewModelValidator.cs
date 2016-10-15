using Ceriyo.Core.Validation;
using Ceriyo.Core.Validation.Data;
using FluentValidation;

namespace Ceriyo.Toolset.WPF.Views.ClassEditorView
{
    public class ClassEditorViewModelValidator: AbstractValidator<ClassEditorViewModel>
    {
        public ClassEditorViewModelValidator()
        {
            RuleForEach(x => x.Classes)
                .SetValidator(new ClassDataValidator());

            RuleFor(x => x.Classes)
                .Must((x) => !ValidationHelper.IsDuplicate(x, "Tag", "Tag must be unique."))
                .WithMessage("Tag must be unique.");

            RuleFor(x => x.Classes)
                .Must((x) => !ValidationHelper.IsDuplicate(x, "Resref", "Resref must be unique."))
                .WithMessage("Resref must be unique.");
        }
    }
}
