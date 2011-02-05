using System.Text;
using Urasandesu.NAnonym;
using Urasandesu.NAnonym.ILTools;

namespace Urasandesu.NTroll.FormulaSample2.Formulas
{
    public abstract class BinaryFormula : Formula, IBinaryFormula
    {
        protected internal BinaryFormula(NodeType nodeType, IFormula left, IFormula right, ITypeDeclaration type, IMethodDeclaration method, IFormula parent)
            : base(nodeType, type, parent)
        {
            Left = left;
            Method = method;
            Right = right;
        }

        public IFormula Left { get; protected set; }
        public IMethodDeclaration Method { get; protected set; }
        public IFormula Right { get; protected set; }

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
