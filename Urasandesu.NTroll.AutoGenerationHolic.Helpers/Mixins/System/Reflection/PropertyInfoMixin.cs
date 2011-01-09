using System.CodeDom;
using System.Reflection;

namespace Urasandesu.NTroll.AutoGenerationHolic.Helpers.Mixins.System.Reflection
{
    public static class PropertyInfoMixin
    {
        public static MemberAttributes GetMemberAttributes(this PropertyInfo source)
        {
            var getter = source.GetGetMethod(true);
            var setter = source.GetSetMethod(true);
            var propertyAttributes = getter.Attributes & setter.Attributes;
            var attributes = default(MemberAttributes);

            if ((propertyAttributes & MethodAttributes.Abstract) == MethodAttributes.Abstract)
            {
                attributes |= MemberAttributes.Abstract;
            }

            if ((propertyAttributes & MethodAttributes.VtableLayoutMask) == MethodAttributes.VtableLayoutMask)
            {
                // デフォルト virtual
            }
            else
            {
                attributes |= MemberAttributes.Final;
            }

            if (getter.IsPublic && setter.IsPublic)
            {
                attributes |= MemberAttributes.Public;
            }
            else
            {
                if ((propertyAttributes & MethodAttributes.Assembly) == MethodAttributes.Assembly)
                {
                    attributes |= MemberAttributes.Assembly;
                }

                if ((propertyAttributes & MethodAttributes.Family) == MethodAttributes.Family)
                {
                    attributes |= MemberAttributes.Family;
                }

                if ((propertyAttributes & MethodAttributes.FamORAssem) == MethodAttributes.FamORAssem)
                {
                    attributes |= MemberAttributes.FamilyOrAssembly;
                }

                if ((propertyAttributes & MethodAttributes.Private) == MethodAttributes.Private)
                {
                    attributes |= MemberAttributes.Private;
                }
            }

            return attributes;
        }
    }
}
