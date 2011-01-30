using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Text;
using Urasandesu.NAnonym;
using Urasandesu.NAnonym.ILTools;
using Urasandesu.NAnonym.Mixins.System;
using Urasandesu.NAnonym.Mixins.System.Reflection;

namespace Urasandesu.NTroll.FormulaSample.Formulas
{
    public class ConvertFormula : UnaryFormula
    {
        protected internal ConvertFormula(Formula operand, ITypeDeclaration type)
            : base(NodeType.Convert, type, null, operand)
        {
        }

        public override Formula Accept(IFormulaVisitor visitor)
        {
            throw new NotImplementedException();
        }
    }
}
