using Ceriyo.Core.Data;
using FluentValidation;

namespace Ceriyo.Core.Validation.Data
{
    public class ItemDataValidator: AbstractValidator<ItemData>
    {
        public ItemDataValidator()
        {

            RuleFor(x => x.GlobalID)
                .NotNull()
                .NotEmpty();

            RuleFor(x => x.Name)
                .NotEmpty()
                .Length(1, 256);

            RuleFor(x => x.Tag)
                .NotEmpty()
                .Length(1, 64);

            RuleFor(x => x.Resref)
                .NotEmpty()
                .Length(1, 32);

            RuleFor(x => x.Comment)
                .Length(0, 5000);

            RuleFor(x => x.Description)
                .Length(0, 2000);

            RuleFor(x => x.ItemTypeResref)
                .NotEmpty()
                .Length(1, 32)
                .WithMessage("An item type must be selected.");

            RuleFor(x => x.OnActivated)
                .Length(0, 32);

            RuleFor(x => x.OnAcquired)
                .Length(0, 32);

            RuleFor(x => x.OnUnacquired)
                .Length(0, 32);

            RuleFor(x => x.OnEquipped)
                .Length(0, 32);

            RuleFor(x => x.OnUnequipped)
                .Length(0, 32);

            RuleForEach(x => x.ItemPropertyResrefs)
                .NotEmpty()
                .Length(1, 32);

            RuleFor(x => x.LocalVariables)
                .SetValidator(new LocalVariableDataValidator());
        }
    }
}
