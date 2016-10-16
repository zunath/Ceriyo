using Ceriyo.Core.Data;
using FluentValidation;

namespace Ceriyo.Core.Validation.Data
{
    public class ClassLevelDataValidator: AbstractValidator<ClassLevelData>
    {
        public ClassLevelDataValidator()
        {
            RuleFor(x => x.ExperienceRequired)
                .NotNull()
                .NotEmpty()
                .GreaterThanOrEqualTo(1);

            RuleFor(x => x.Level)
                .InclusiveBetween(1, 99);
        }
    }
}
