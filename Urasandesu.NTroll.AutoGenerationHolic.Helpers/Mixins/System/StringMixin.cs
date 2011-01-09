using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Urasandesu.NTroll.AutoGenerationHolic.Helpers.Mixins.System
{
    public static class StringMixin
    {
        public static string ToCamel(this string source)
        {
            return source.Length <= 1 ? source.ToLower() : source.Substring(0, 1).ToLower() + source.Substring(1);
        }
    }
}
