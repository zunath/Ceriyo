using Ceriyo.Core.Data;
using FluentValidation;

namespace Ceriyo.Core.Validation.Data
{
    public class LocalDoubleDataValidator: AbstractValidator<LocalDoubleData>
    {
        public LocalDoubleDataValidator()
        {
            RuleFor(x => x.Key)
                .NotNull()
                .NotEmpty();
        }
    }
}
