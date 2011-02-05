using System.Text;
using Urasandesu.NAnonym;
using Urasandesu.NAnonym.ILTools;
using System;
using Urasandesu.NAnonym.Mixins.System;

namespace Urasandesu.NTroll.FormulaSample2.Formulas
{
    public abstract class UnaryFormula : Formula, IUnaryFormula
    {
        protected internal UnaryFormula(NodeType nodeType, ITypeDeclaration type, IFormula operand, IMethodDeclaration method, IFormula parent)
            : base(nodeType, type, parent)
        {
            Method = method;
            Operand = operand;
        }

        public IMethodDeclaration Method { get; protected set; }
        public IFormula Operand { get; protected set; }

        protected override void AppendToString(StringBuilder sb)
        {
            base.AppendToString(sb);
            sb.Append(", ");
            sb.Append("\"Method\": ");
            AppendMethodToString(sb);
            sb.Append(", ");
            sb.Append("\"Operand\": ");
            sb.Append(Operand.NullableToString());
        }

        protected abstract void AppendMethodToString(StringBuilder sb);
    }
}
