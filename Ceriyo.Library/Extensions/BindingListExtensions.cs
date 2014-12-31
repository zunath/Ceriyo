using System;
using System.ComponentModel;
using System.Linq;

namespace Ceriyo.Library.Extensions
{
    public static class BindingListExtensions
    {
        public static void RemoveAll<T>(this BindingList<T> list, Func<T, bool> predicate)
        {
            foreach (var item in list.Where(predicate).ToArray())
            {
                list.Remove(item);
            }
        }
    }
}
