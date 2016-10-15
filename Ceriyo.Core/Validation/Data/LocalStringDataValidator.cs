using Ceriyo.Core.Data;
using FluentValidation;

namespace Ceriyo.Core.Validation.Data
{
    public class LocalStringDataValidator: AbstractValidator<LocalStringData>
    {
        public LocalStringDataValidator()
        {
            RuleFor(x => x.Key)
                .NotNull()
                .NotEmpty();

            RuleFor(x => x.Value)
                .NotNull();
        }

    }
}
