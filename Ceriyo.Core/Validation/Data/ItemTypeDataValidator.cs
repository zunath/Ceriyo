using Ceriyo.Core.Data;
using FluentValidation;

namespace Ceriyo.Core.Validation.Data
{
    public class ItemTypeDataValidator: AbstractValidator<ItemTypeData>
    {
        public ItemTypeDataValidator()
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
        }
    }
}
