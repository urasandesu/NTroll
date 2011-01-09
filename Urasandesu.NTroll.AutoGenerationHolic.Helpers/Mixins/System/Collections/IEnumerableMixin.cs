using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Urasandesu.NTroll.AutoGenerationHolic.Helpers.Mixins.System.Collections
{
    public static class IEnumerableMixin
    {
        public static TSource FirstOrDefault<TSource>(this IEnumerable source, Func<TSource, bool> predicate)
        {
            return Enumerable.FirstOrDefault<TSource>(source.Cast<TSource>(), predicate);
        }

        public static IEnumerable<TResult> Select<TSource, TResult>(this IEnumerable source, Func<TSource, TResult> selector)
        {
            return Enumerable.Select<TSource, TResult>(source.Cast<TSource>(), selector);
        }
    }
}
