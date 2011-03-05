using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Urasandesu.NTroll.FormulaSample5.Formulas
{
    public partial class ReflectivePropertyFormula : PropertyFormula
    {
        public ReflectivePropertyFormula(Formula instance, PropertyInfo pi)
            : base(instance, pi)
        {
        }
    }
}
