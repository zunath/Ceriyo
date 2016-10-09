using Ceriyo.Core.Data;
using FluentValidation;

namespace Ceriyo.Core.Validators.Data
{
    public class FrameDataValidator: AbstractValidator<FrameData>
    {
        public FrameDataValidator()
        {
            RuleFor(x => x.GlobalID)
                .NotNull()
                .NotEmpty();

            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty()
                .Length(1, 256);

            RuleFor(x => x.FrameLength)
                .GreaterThanOrEqualTo(0.0f);

            RuleFor(x => x.TextureCellX)
                .GreaterThanOrEqualTo(0);

            RuleFor(x => x.TextureCellY)
                .GreaterThanOrEqualTo(0);
        }
    }
}
