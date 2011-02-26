using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Urasandesu.NAnonym.ILTools;
using System.Reflection;
using Urasandesu.NAnonym.Mixins.System.Reflection;
using Urasandesu.NAnonym.Linq;

namespace Urasandesu.NTroll.FormulaSample5.Formulas
{
    public partial class NewFormula : Formula
    {
        public NewFormula(ConstructorInfo ci, Formula[] arguments)
            : this()
        {
            Constructor = ci.ToConstructorDecl();
            arguments.AddRangeTo(Arguments);
        }
    }
}
