using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Urasandesu.NAnonym.Mixins.System;

namespace Urasandesu.NTroll.FormulaSample5.Formulas
{
    public partial class TypeAsFormula : UnaryFormula
    {
        public TypeAsFormula(Formula operand, Type type)
        {
            Operand = operand;
            TypeDeclaration = type.ToTypeDecl();
        }

        public override string MethodToStringValueIfDefault
        {
            get { return null; }
        }
    }
}
