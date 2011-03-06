using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Urasandesu.NAnonym.Mixins.System;
using Urasandesu.NAnonym.ILTools;

namespace Urasandesu.NTroll.FormulaSample5.Formulas
{
    public partial class ConvertFormula : UnaryFormula
    {
        public ConvertFormula(Formula operand, Type type)
            : this(operand, type.ToTypeDecl())
        {
        }

        public ConvertFormula(Formula operand, ITypeDeclaration typeDecl)
        {
            Operand = operand;
            TypeDeclaration = typeDecl;
        }

        public override string MethodToStringValueIfDefault
        {
            get { return null; }
        }
    }
}
