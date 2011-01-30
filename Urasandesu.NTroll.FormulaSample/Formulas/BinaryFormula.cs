using System;
using System.Text;
using Urasandesu.NAnonym;
using Urasandesu.NAnonym.ILTools;

namespace Urasandesu.NTroll.FormulaSample.Formulas
{
    public abstract class BinaryFormula : Formula
    {
        protected internal BinaryFormula(NodeType nodeType, Formula left, IMethodDeclaration method, Formula right)
            : base(nodeType, left.Type)
        {
            Left = left;
            Method = method;
            Right = right;
        }

        public Formula Left { get; private set; }
        public IMethodDeclaration Method { get; private set; }
        public Formula Right { get; private set; }

        public override Formula Accept(IFormulaVisitor visitor)
        {
            throw new NotImplementedException();
        }

        protected override void AppendToString(StringBuilder sb)
        {
            base.AppendToString(sb);
            sb.Append(", ");
            sb.Append("\"Left\": ");
            sb.Append(Left.NullableToString());
            sb.Append(", ");
            sb.Append("\"Method\": ");
            AppendMethodToString(sb);
            sb.Append(", ");
            sb.Append("\"Right\": ");
            sb.Append(Right.NullableToString());
        }

        protected abstract void AppendMethodToString(StringBuilder sb);
    }
}
