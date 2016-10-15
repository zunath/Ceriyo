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
                .Must(NotADuplicate)
                .WithMessage("Keys must be unique.");

            RuleFor(x => x.LocalFloats)
                .Must(NotADuplicate)
                .WithMessage("Keys must be unique.");

            RuleForEach(x => x.LocalStrings)
                .SetValidator(new LocalStringDataValidator());

            RuleForEach(x => x.LocalFloats)
                .SetValidator(new LocalFloatDataValidator());
        }


        private static bool NotADuplicate(IEnumerable<LocalStringData> localStrings)
        {
            var list = localStrings.ToList();

            foreach (var str in list)
            {
                str.ClearExternalError(nameof(str.Key));
            }

            var dupes =
                (from s in list.Where(x => !string.IsNullOrWhiteSpace(x.Key))
                 group s by s.Key
                 into grouped
                 where grouped.Skip(1).Any()
                 from s in grouped
                 select s).ToList();

            foreach (var dupe in dupes)
            {
                dupe.SetExternalError(nameof(dupe.Key), "Keys must be unique.");
            }

            return !dupes.Any();
        }

        private static bool NotADuplicate(IEnumerable<LocalFloatData> localFloats)
        {
            var dupes =
                (from s in localFloats.Where(x => !string.IsNullOrWhiteSpace(x.Key))
                 group s by s.Key
                 into grouped
                 where grouped.Skip(1).Any()
                 from s in grouped
                 select s).ToList();

            return !dupes.Any();
        }
    }
}
