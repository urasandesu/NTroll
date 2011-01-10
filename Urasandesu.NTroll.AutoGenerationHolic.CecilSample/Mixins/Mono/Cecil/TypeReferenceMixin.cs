using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mono.Cecil;

namespace Urasandesu.NTroll.AutoGenerationHolic.CecilSample.Mixins.Mono.Cecil
{
    public static class TypeReferenceMixin
    {
        public static MethodReference GetMethodOpEquality(this TypeReference source)
        {
            return source.Resolve().GetMethodOpEquality();
        }

        public static bool HasOptimizedOpEquality(this TypeReference source)
        {
            var typeFullName = source.FullName;
            if (typeFullName == typeof(byte).FullName || typeFullName == typeof(sbyte).FullName ||
                typeFullName == typeof(short).FullName || typeFullName == typeof(ushort).FullName ||
                typeFullName == typeof(int).FullName || typeFullName == typeof(uint).FullName ||
                typeFullName == typeof(long).FullName || typeFullName == typeof(ulong).FullName ||
                typeFullName == typeof(float).FullName || typeFullName == typeof(double).FullName)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
