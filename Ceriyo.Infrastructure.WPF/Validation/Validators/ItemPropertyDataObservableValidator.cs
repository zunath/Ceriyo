using Ceriyo.Infrastructure.WPF.Observables;
using FluentValidation;

namespace Ceriyo.Infrastructure.WPF.Validation.Validators
{
    public class ItemPropertyDataObservableValidator: AbstractValidator<ItemPropertyDataObservable>
    {
        public ItemPropertyDataObservableValidator()
        {
            RuleFor(x => x.GlobalID)
                .NotNull()
                .NotEmpty();

            RuleFor(x => x.Name)
                .NotEmpty()
                .Length(1, 256);

            RuleFor(x => x.Tag)
                .NotEmpty()
                .Length(1, 64);

            RuleFor(x => x.Resref)
                .NotEmpty()
                .Length(1, 32);

            RuleFor(x => x.Comment)
                .Length(0, 5000);

            RuleFor(x => x.Description)
                .Length(0, 2000);
        }
    }
}
