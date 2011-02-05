using System;
using System.Text;
using Urasandesu.NAnonym;
using Urasandesu.NAnonym.ILTools;

namespace Urasandesu.NTroll.FormulaSample2.Formulas
{
    public class ConstantFormula : Formula, IConstantFormula
    {
        protected internal ConstantFormula(object value, ITypeDeclaration type, IFormula parent)
            : base(NodeType.Constant, type, parent)
        {
            Value = value;
        }

        public object Value { get; protected set; }

        public override IFormula Accept(IFormulaVisitor visitor)
        {
            throw new NotImplementedException();
        }

        protected override void AppendToString(StringBuilder sb)
        {
            base.AppendToString(sb);
            sb.Append(", ");
            sb.Append("\"Value\": ");
            sb.Append("\"");
            sb.Append(Value.NullableToString());
            sb.Append("\"");
        }
    }
}
