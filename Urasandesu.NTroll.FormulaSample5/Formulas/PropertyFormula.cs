using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Urasandesu.NAnonym.Mixins.System.Reflection;
using Urasandesu.NAnonym.ILTools;

namespace Urasandesu.NTroll.FormulaSample5.Formulas
{
    public partial class PropertyFormula : MemberFormula
    {
        public PropertyFormula(Formula instance, PropertyInfo pi)
            : this()
        {
            Instance = instance;
            Member = pi.ToPropertyDecl();
        }
    }
}
