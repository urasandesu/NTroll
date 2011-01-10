using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mono.Cecil;

namespace Urasandesu.NTroll.AutoGenerationHolic.CecilSample.Mixins.Mono.Cecil
{
    public static class AssemblyDefinitionMixin
    {
        public static IEnumerable<TypeDefinition> GetTypesDefined<TAttribute>(this AssemblyDefinition source)
        {
            var attributeFullName = typeof(TAttribute).FullName;
            return source.MainModule.Types.Where(t => t.CustomAttributes.Any(a => a.AttributeType.FullName == attributeFullName));
        }
    }
}
