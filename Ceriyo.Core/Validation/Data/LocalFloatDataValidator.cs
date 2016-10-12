using Ceriyo.Core.Data;
using FluentValidation;

namespace Ceriyo.Core.Validation.Data
{
    public class LocalFloatDataValidator: AbstractValidator<LocalFloatData>
    {
        public LocalFloatDataValidator()
        {
            RuleFor(x => x.Key)
                .NotNull()
                .NotEmpty();
        }
    }
}
