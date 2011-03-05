using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Urasandesu.NAnonym.Mixins.System.Reflection;
using System.ComponentModel;

namespace Urasandesu.NTroll.FormulaSample5.Formulas
{
    public partial class FieldFormula : MemberFormula
    {
        public FieldFormula(Formula instance, FieldInfo fi)
        {
            Instance = instance;
            Member = fi.ToFieldDecl();
        }

        protected override bool ReceivePropertyChangedCore(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == NameOfMember)
            {
                TypeDeclaration = Member == null ? null : Member.FieldType;
            }
            return base.ReceivePropertyChangedCore(sender, e);
        }
    }
}
