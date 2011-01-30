using System.Text;
using Urasandesu.NAnonym.ILTools;

namespace Urasandesu.NTroll.FormulaSample.Formulas
{
    public class AssignFormula : BinaryFormula
    {
        protected internal AssignFormula(Formula left, IMethodDeclaration method, Formula right)
            : base(NodeType.Assign, left, method, right)
        {
        }

        public override Formula Accept(IFormulaVisitor visitor)
        {
            return base.Accept(visitor);
        }

        protected override void AppendMethodToString(StringBuilder sb)
        {
            if (Method == null)
            {
                sb.Append("\"=\"");
            }
            else
            {
                sb.Append("\"");
                sb.Append(Method.ToString());
                sb.Append("\"");
            }
        }
    }
}
