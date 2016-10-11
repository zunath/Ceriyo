using Ceriyo.Core.Data;
using FluentValidation;

namespace Ceriyo.Core.Validation.Data
{
    public class LocalVariableDataValidator: AbstractValidator<LocalVariableData>
    {
        public LocalVariableDataValidator()
        {

            RuleFor(x => x.GlobalID)
                .NotNull()
                .NotEmpty();

            RuleForEach(x => x.LocalStrings)
                .NotNull();
        }
    }
}
