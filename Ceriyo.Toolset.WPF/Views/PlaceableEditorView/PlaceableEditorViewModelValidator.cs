using Ceriyo.Core.Validation;
using Ceriyo.Core.Validation.Data;
using FluentValidation;

namespace Ceriyo.Toolset.WPF.Views.PlaceableEditorView
{
    public class PlaceableEditorViewModelValidator: AbstractValidator<PlaceableEditorViewModel>
    {
        public PlaceableEditorViewModelValidator()
        {
            RuleForEach(x => x.Placeables)
                .SetValidator(new PlaceableDataValidator());

            RuleFor(x => x.Placeables)
                .Must((x) => !ValidationHelper.IsDuplicate(x, "Tag", "Tag must be unique."))
                .WithMessage("Tag must be unique.");

            RuleFor(x => x.Placeables)
                .Must((x) => !ValidationHelper.IsDuplicate(x, "Resref", "Resref must be unique."))
                .WithMessage("Resref must be unique.");
        }
    }
}
