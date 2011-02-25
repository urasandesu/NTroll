using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Urasandesu.NAnonym;
using Urasandesu.NAnonym.Mixins.System;

namespace Urasandesu.NTroll.FormulaSample5.Formulas
{
    public partial class ConstantFormula : Formula
    {
        public ConstantFormula(object value, Type type)
            : this()
        {
            ConstantValue = value;
            TypeDeclaration = type.ToTypeDecl();
        }
    }
}
