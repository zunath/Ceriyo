using Ceriyo.Core.Validation;
using Ceriyo.Core.Validation.Data;
using FluentValidation;

namespace Ceriyo.Toolset.WPF.Views.ItemEditorView
{
    public class ItemEditorViewModelValidator: AbstractValidator<ItemEditorViewModel>
    {
        public ItemEditorViewModelValidator()
        {
            RuleForEach(x => x.Items)
                .SetValidator(new ItemDataValidator());

            RuleFor(x => x.Items)
                .Must((x) => !ValidationHelper.IsDuplicate(x, "Tag", "Tag must be unique."))
                .WithMessage("Tag must be unique.");

            RuleFor(x => x.Items)
                .Must((x) => !ValidationHelper.IsDuplicate(x, "Resref", "Resref must be unique."))
                .WithMessage("Resref must be unique.");
        }
    }
}
