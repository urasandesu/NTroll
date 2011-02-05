using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Urasandesu.NAnonym.ILTools;
using Urasandesu.NAnonym;

namespace Urasandesu.NTroll.FormulaSample2.Formulas
{
    public class VariableFormula : Formula, IVariableFormula
    {
        protected internal VariableFormula(ITypeDeclaration type, string name, IFormula parent)
            : base(NodeType.Variable, type, parent)
        {
            Name = name;
        }

        public string Name { get; protected set; }

        public override IFormula Accept(IFormulaVisitor visitor)
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
