using System.Collections.Generic;

namespace Ceriyo.Infrastructure.WPF.Validation.Contracts
{
    public interface IValidationHelper
    {
        bool IsDuplicate(IEnumerable<object> objectSet, string propertyName, string errorMessage);
    }
}
