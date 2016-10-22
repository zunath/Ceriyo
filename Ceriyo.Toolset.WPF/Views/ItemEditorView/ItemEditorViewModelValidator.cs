using Ceriyo.Infrastructure.WPF.Validation.Validators;
using FluentValidation;

namespace Ceriyo.Toolset.WPF.Views.ItemEditorView
{
    public class ItemEditorViewModelValidator: AbstractValidator<ItemEditorViewModel>
    {
        public ItemEditorViewModelValidator()
        {
            RuleForEach(x => x.Items)
                .SetValidator(new ItemDataObservableValidator());

            //RuleFor(x => x.Items)
            //    .Must((x) => !ValidationHelper.IsDuplicate(x, "Tag", "Tag must be unique."))
            //    .WithMessage("Tag must be unique.");

            //RuleFor(x => x.Items)
            //    .Must((x) => !ValidationHelper.IsDuplicate(x, "Resref", "Resref must be unique."))
            //    .WithMessage("Resref must be unique.");
        }
    }
}
