using System;
using System.Text;
using Urasandesu.NAnonym;
using Urasandesu.NAnonym.ILTools;

namespace Urasandesu.NTroll.FormulaSample2.Formulas
{
    public class ReturnFormula : Formula, IReturnFormula
    {
        protected internal ReturnFormula(IFormula @object, ITypeDeclaration type, IFormula parent)
            : base(NodeType.Return, type, parent)
        {
            Object = @object;
        }

        public IFormula Object { get; protected set; }

        public override IFormula Accept(IFormulaVisitor visitor)
        {
            throw new NotImplementedException();
        }

        protected override void AppendToString(StringBuilder sb)
        {
            base.AppendToString(sb);
            sb.Append(", ");
            sb.Append("\"Object\": ");
            sb.Append(Object.NullableToString());
        }
    }
}
