using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Urasandesu.NTroll.FormulaSample5.Formulas
{
    public partial class ReflectiveCallFormula : CallFormula
    {
        public ReflectiveCallFormula(Formula instance, MethodInfo mi, Formula[] arguments)
            : base(instance, mi, arguments)
        {
        }
    }
}
