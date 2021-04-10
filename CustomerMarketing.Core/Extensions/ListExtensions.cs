using System;
using System.Collections.Generic;
using System.Linq;

namespace CustomerMarketing.Core.Extensions
{
    public static class ListExtensions
    {
        public static TProperty? FirstItemPropertyOrDefault<T, TProperty>(this IEnumerable<T> list,
            Func<T, TProperty> reducer)
        {
            if (reducer == null) throw new ArgumentNullException(nameof(reducer));

            var enumerable = list as T[] ?? list.ToArray();
            return enumerable.Any() ? reducer(enumerable.First()) : default;
        }
    }
}