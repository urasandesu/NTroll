using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Urasandesu.NAnonym.ILTools;
using Urasandesu.NAnonym.Mixins.System;

namespace Urasandesu.NTroll.FormulaSample2.Formulas
{
    public class ConvertFormula : UnaryFormula, IConvertFormula
    {
        protected internal ConvertFormula(ITypeDeclaration type, IFormula operand, IMethodDeclaration method, IFormula parent)
            : base(NodeType.Convert, type, operand, method, parent)
        {
        }

        protected override void AppendMethodToString(StringBuilder sb)
        {
            if (Method == null)
            {
                if (Operand == null)
                {
                    sb.Append("null");
                }
                else if (typeof(object).Equivalent(Type) && typeof(ValueType).IsAssignableFrom(Operand.Type))
                {
                    sb.Append("\"Box\"");
                }
                else
                {
                    sb.Append("null");
                }
            }
            else
            {
                sb.Append("\"");
                sb.Append(Method.ToString());
                sb.Append("\"");
            }
        }

        public override IFormula Accept(IFormulaVisitor visitor)
        {
            throw new NotImplementedException();
        }
    }
}
