using Ceriyo.Core.Data;
using FluentValidation;

namespace Ceriyo.Core.Validation.Data
{
    public class ScriptDataValidator: AbstractValidator<ScriptData>
    {
        public ScriptDataValidator()
        {

            RuleFor(x => x.GlobalID)
                .NotNull()
                .NotEmpty();

            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty()
                .Length(1, 256);

            RuleFor(x => x.Resref)
                .NotNull()
                .NotEmpty()
                .Length(1, 32);
        }
    }
}
