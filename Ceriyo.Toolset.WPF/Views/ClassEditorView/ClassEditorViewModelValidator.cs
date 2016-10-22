using Ceriyo.Infrastructure.WPF.Validation.Validators;
using FluentValidation;

namespace Ceriyo.Toolset.WPF.Views.ClassEditorView
{
    public class ClassEditorViewModelValidator: AbstractValidator<ClassEditorViewModel>
    {
        public ClassEditorViewModelValidator()
        {
            RuleForEach(x => x.Classes)
                .SetValidator(new ClassDataObservableValidator());

            //RuleFor(x => x.Classes)
            //    .Must((x) => !ValidationHelper.IsDuplicate(x, "Tag", "Tag must be unique."))
            //    .WithMessage("Tag must be unique.");

            //RuleFor(x => x.Classes)
            //    .Must((x) => !ValidationHelper.IsDuplicate(x, "Resref", "Resref must be unique."))
            //    .WithMessage("Resref must be unique.");
        }
    }
}
