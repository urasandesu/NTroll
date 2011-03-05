using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Urasandesu.NAnonym.Mixins.System.Reflection;
using Urasandesu.NAnonym.ILTools;
using System.ComponentModel;

namespace Urasandesu.NTroll.FormulaSample5.Formulas
{
    public partial class PropertyFormula : MemberFormula
    {
        public PropertyFormula(Formula instance, PropertyInfo pi)
        {
            Instance = instance;
            Member = pi.ToPropertyDecl();
        }

        protected override bool ReceivePropertyChangedCore(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == NameOfMember)
            {
                TypeDeclaration = Member == null ? null : Member.PropertyType;
            }
            return base.ReceivePropertyChangedCore(sender, e);
        }
    }
}
