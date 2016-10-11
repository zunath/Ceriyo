using Ceriyo.Core.Data;
using FluentValidation;

namespace Ceriyo.Core.Validation.Data
{
    public class CreatureDataValidator: AbstractValidator<CreatureData>
    {
        public CreatureDataValidator()
        {
            RuleFor(x => x.GlobalID)
                .NotNull()
                .NotEmpty();

            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty()
                .Length(1, 256);

            RuleFor(x => x.Tag)
                .NotNull()
                .NotEmpty()
                .Length(1, 64);

            RuleFor(x => x.Resref)
                .NotNull()
                .NotEmpty()
                .Length(1, 32);
            
            RuleFor(x => x.Comment)
                .Length(0, 5000);

            RuleFor(x => x.Description)
                .Length(0, 2000);

            RuleFor(x => x.Level)
                .InclusiveBetween(1, 99);

            RuleFor(x => x.ClassResref)
                .NotNull()
                .NotEmpty()
                .Length(1, 32);

            RuleFor(x => x.LocalVariables)
                .NotNull();


        }
    }
}
