using System.Collections.Generic;
using System.Linq;
using FluentValidation;
using FluentValidation.Internal;
using FluentValidation.Results;

namespace Ceriyo.Infrastructure.WPF.Validation
{
    public static class ValidationExtensions
    {
        public static ValidationResult ValidateProperty(this IValidator validator, object target, string propertyName)
        {
            var context = new ValidationContext(target, new PropertyChain(),
                new MemberNameValidatorSelector(new[] { propertyName }));
            return validator.Validate(context);
        }

        public static ValidationResult ValidateProperties(this IValidator validator, object target, string[] propertyNames)
        {
            var context = new ValidationContext(target, new PropertyChain(), 
                new MemberNameValidatorSelector(propertyNames));

            return validator.Validate(context);
        }

        public static IEnumerable<string> ValidatePropertyAndReturnErrors(this IValidator validator, object target, string propertyName)
        {
            var result = validator.ValidateProperty(target, propertyName);
            return result.Errors.Select(x => x.ErrorMessage);
        }
    }
}
