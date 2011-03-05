using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Urasandesu.NTroll.FormulaSample5.Formulas
{
    public partial class ReflectiveNewFormula : NewFormula
    {
        public ReflectiveNewFormula(ConstructorInfo ci, Formula[] arguments)
            : base(ci, arguments)
        {
        }
    }
}
