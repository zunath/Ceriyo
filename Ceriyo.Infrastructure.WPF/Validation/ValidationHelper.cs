using System.Collections.Generic;
using System.Linq;
using Ceriyo.Infrastructure.WPF.Validation.Contracts;

namespace Ceriyo.Infrastructure.WPF.Validation
{
    public class ValidationHelper: IValidationHelper
    {
        public bool IsDuplicate(IEnumerable<object> objectSet, string propertyName, string errorMessage)
        {

            // TODO: Figure out how to set external errors more efficiently
            var dupes =
                (from s in objectSet.Where(x => !string.IsNullOrWhiteSpace((string)x.GetType().GetProperty(propertyName).GetValue(x, null)))
                 group s by (string)s.GetType().GetProperty(propertyName).GetValue(s, null)
                 into grouped
                 where grouped.Skip(1).Any()
                 from s in grouped
                 select s).ToList();
            
            return dupes.Any();
        }
    }
}
