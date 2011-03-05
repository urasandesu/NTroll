using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Urasandesu.NTroll.FormulaSample5.Formulas
{
    public partial class ReflectiveFieldFormula : FieldFormula
    {
        public ReflectiveFieldFormula(Formula instance, FieldInfo fi)
            : base(instance, fi)
        {
        }
    }
}
