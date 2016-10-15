using System.Collections.Generic;
using System.Linq;
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
            
            RuleFor(x => x.LocalStrings)
                .Must((x) => !ValidationHelper.IsDuplicate(x, "Key", "Keys must be unique."))
                .WithMessage("Keys must be unique.");

            RuleFor(x => x.LocalDoubles)
                .Must((x) => !ValidationHelper.IsDuplicate(x, "Key", "Keys must be unique."))
                .WithMessage("Keys must be unique.");

            RuleForEach(x => x.LocalStrings)
                .SetValidator(new LocalStringDataValidator());

            RuleForEach(x => x.LocalDoubles)
                .SetValidator(new LocalDoubleDataValidator());
        }
    }
}
