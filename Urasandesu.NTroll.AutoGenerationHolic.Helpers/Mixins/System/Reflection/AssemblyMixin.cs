using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Urasandesu.NTroll.AutoGenerationHolic.Helpers.Mixins.System.Reflection
{
    public static class AssemblyMixin
    {
        public static IEnumerable<Type> GetTypesDefined<TAttribute>(this Assembly source)
        {
            foreach (var t in source.GetTypes().Where(t => t.IsDefined(typeof(TAttribute), false)))
            {
                yield return t;
            }
        }
    }
}
