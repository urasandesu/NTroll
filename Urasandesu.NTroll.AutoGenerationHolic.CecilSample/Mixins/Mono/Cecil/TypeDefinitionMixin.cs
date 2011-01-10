using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mono.Cecil;

namespace Urasandesu.NTroll.AutoGenerationHolic.CecilSample.Mixins.Mono.Cecil
{
    public static class TypeDefinitionMixin
    {
        public static IEnumerable<PropertyDefinition> GetPropertiesDefined<TAttribute>(this TypeDefinition source)
        {
            var attributeFullName = typeof(TAttribute).FullName;
            return source.Properties.Where(p => p.CustomAttributes.Any(a => a.AttributeType.FullName == attributeFullName));
        }

        public static MethodDefinition GetMethodOpEquality(this TypeDefinition source)
        {
            return source.Methods.FirstOrDefault(
                m => m.Name == "op_Equality" && m.Parameters.All(p => p.ParameterType.FullName == source.FullName));
        }
    }
}
