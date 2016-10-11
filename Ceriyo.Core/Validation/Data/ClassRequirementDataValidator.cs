using Ceriyo.Core.Data;
using FluentValidation;

namespace Ceriyo.Core.Validation.Data
{
    public class ClassRequirementDataValidator: AbstractValidator<ClassRequirementData>
    {
        public ClassRequirementDataValidator()
        {
            RuleFor(x => x.ClassResref)
                .NotNull()
                .NotEmpty()
                .Length(1, 32);

            RuleFor(x => x.LevelRequired)
                .InclusiveBetween(1, 99);
        }
    }
}
