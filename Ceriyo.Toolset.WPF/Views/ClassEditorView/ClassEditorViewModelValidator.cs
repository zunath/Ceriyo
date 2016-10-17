using Ceriyo.Infrastructure.WPF.Validation;
using Ceriyo.Infrastructure.WPF.Validation.Validators;
using FluentValidation;

namespace Ceriyo.Toolset.WPF.Views.ClassEditorView
{
    public class ClassEditorViewModelValidator: AbstractValidator<ClassEditorViewModel>
    {
        public ClassEditorViewModelValidator(ClassDataObservableValidator classValidator)
        {
            RuleForEach(x => x.Classes)
                .SetValidator(classValidator);

            //RuleFor(x => x.Classes)
            //    .Must((x) => !ValidationHelper.IsDuplicate(x, "Tag", "Tag must be unique."))
            //    .WithMessage("Tag must be unique.");

            //RuleFor(x => x.Classes)
            //    .Must((x) => !ValidationHelper.IsDuplicate(x, "Resref", "Resref must be unique."))
            //    .WithMessage("Resref must be unique.");
        }
    }
}
