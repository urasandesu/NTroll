using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Urasandesu.NAnonym.Linq;
using Urasandesu.NAnonym.Mixins.System.Reflection;

namespace Urasandesu.NTroll.FormulaSample5.Formulas
{
    public partial class CallFormula : Formula
    {
        public CallFormula(Formula instance, MethodInfo mi, Formula[] arguments)
            : this()
        {
            Instance = instance;
            Method = mi.ToMethodDecl();
            arguments.AddRangeTo(Arguments);
        }
    }
}
