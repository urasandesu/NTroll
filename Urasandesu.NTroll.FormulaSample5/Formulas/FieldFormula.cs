using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Urasandesu.NAnonym.Mixins.System.Reflection;

namespace Urasandesu.NTroll.FormulaSample5.Formulas
{
    public partial class FieldFormula : MemberFormula
    {
        public FieldFormula(Formula instance, FieldInfo fi)
            : this()
        {
            Instance = instance;
            Member = fi.ToFieldDecl();
        }
    }
}
