﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.CodeDom;

namespace Urasandesu.NTroll.AutoGenerationHolic.Helpers.Mixins.System
{
    public static class TypeMixin
    {
        public static IEnumerable<PropertyInfo> GetPropertiesDefined<TAttribute>(this Type source)
        {
            return source.GetProperties().Where(_ => _.IsDefined(typeof(TAttribute), false));
        }

        public static MemberAttributes GetMemberAttributes(this Type source)
        {
            var attributes = default(MemberAttributes);

            if (source.IsAbstract)
            {
                attributes |= MemberAttributes.Abstract;
            }

            if (source.IsSealed)
            {
                attributes |= MemberAttributes.Final;
            }

            if ((source.Attributes & TypeAttributes.Public) == TypeAttributes.Public)
            {
                attributes |= MemberAttributes.Public;
            }

            if ((source.Attributes & TypeAttributes.NestedAssembly) == TypeAttributes.NestedAssembly)
            {
                attributes |= MemberAttributes.Assembly;
            }

            if ((source.Attributes & TypeAttributes.NestedFamily) == TypeAttributes.NestedFamily)
            {
                attributes |= MemberAttributes.Family;
            }

            if ((source.Attributes & TypeAttributes.NestedFamORAssem) == TypeAttributes.NestedFamORAssem)
            {
                attributes |= MemberAttributes.FamilyOrAssembly;
            }

            if ((source.Attributes & TypeAttributes.NotPublic) == TypeAttributes.NotPublic)
            {
                attributes |= MemberAttributes.Private;
            }

            return attributes;
        }
    }
}
