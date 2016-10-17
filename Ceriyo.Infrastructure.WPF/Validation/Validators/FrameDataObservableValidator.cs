using Ceriyo.Infrastructure.WPF.Observables;
using FluentValidation;

namespace Ceriyo.Infrastructure.WPF.Validation.Validators
{
    public class FrameDataObservableValidator: AbstractValidator<FrameDataObservable>
    {
        public FrameDataObservableValidator()
        {
            RuleFor(x => x.GlobalID)
                .NotNull()
                .NotEmpty();

            RuleFor(x => x.Name)
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
