using Ceriyo.Core.Entities;
using FluentValidation;

namespace Ceriyo.Core.Validators.Components
{
    public class ClassLevelValidator: AbstractValidator<ClassLevel>
    {
        public ClassLevelValidator()
        {
            RuleFor(x => x.Level)
                .InclusiveBetween(1, 99);

            RuleFor(x => x.ExperienceRequired)
                .GreaterThanOrEqualTo(1);
        }
    }
}
