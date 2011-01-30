using System;
using System.Text;
using Urasandesu.NAnonym;
using Urasandesu.NAnonym.ILTools;

namespace Urasandesu.NTroll.FormulaSample.Formulas
{
    public class VariableFormula : Formula
    {
        protected internal VariableFormula(ITypeDeclaration type, string name)
            : base(NodeType.Variable, type)
        {
            Name = name;
        }

        public string Name { get; private set; }

        public override Formula Accept(IFormulaVisitor visitor)
        {
            throw new NotImplementedException();
        }

        protected override void AppendToString(StringBuilder sb)
        {
            base.AppendToString(sb);
            sb.Append(", ");
            sb.Append("\"Name\": ");
            sb.Append("\"");
            sb.Append(Name.NullableToString());
            sb.Append("\"");
        }
    }
}
