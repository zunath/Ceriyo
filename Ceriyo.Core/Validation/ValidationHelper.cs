using System.Collections.Generic;
using System.Linq;

namespace Ceriyo.Core.Validation
{
    public class ValidationHelper
    {
        public static bool IsDuplicate<T>(IEnumerable<T> objectSet, string propertyName, string errorMessage)
            where T: BaseValidatable
        {
            var list = objectSet.ToList();

            foreach (var str in list)
            {
                str.ClearExternalError(propertyName);
            }

            var dupes =
                (from s in list.Where(x => !string.IsNullOrWhiteSpace((string)x.GetType().GetProperty(propertyName).GetValue(x, null)))
                 group s by (string)s.GetType().GetProperty(propertyName).GetValue(s, null)
                 into grouped
                 where grouped.Skip(1).Any()
                 from s in grouped
                 select s).ToList();

            foreach (var dupe in dupes)
            {
                dupe.SetExternalError(propertyName, errorMessage);
            }

            return dupes.Any();
        }
    }
}
