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
            : this()
        {
            Operand = operand;
            TypeDeclaration = type.ToTypeDecl();
        }

        public override string MethodDefaultExpandString
        {
            get { return null; }
        }
    }
}
