using System;
using System.Collections.Generic;
using System.Linq;

namespace Common
{
    public static class EnumerableExtensions
    {
        public static void Each<T>(this IEnumerable<T> items, Action<T> action)
        {
            var localItems = (items ?? Enumerable.Empty<T>()).ToArray();
            foreach (var item in localItems) action(item);
        }

        public static void EachWithIndex<T>(this IEnumerable<T> items, Action<T,int> action)
        {
            var localItems = items.ToArray();
            for (var i = 0; i < localItems.Length; i++)
            {
                action(localItems[i], i);
            }
        }

        public static bool IsEmpty<T>(this IEnumerable<T> items)
        {
            return items == null || !items.Any();
        }

        public static bool IsNotEmpty<T>(this IEnumerable<T> items) 
        {
            return !items.IsEmpty();
        }

        public static IEnumerable<T> Union<T>(this IEnumerable<T> items, T newItem) 
        {
            return items.Union(new[] {newItem});
        }

        public static bool HasAny<T>(this IEnumerable<T> items, Func<T, bool> predicate) 
        {
            return (items ?? Enumerable.Empty<T>()).Any(predicate);
        }
    }
}