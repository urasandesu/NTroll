using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Urasandesu.NTroll.FormulaSample5.Formulas
{
    public class AssignFormula : BinaryFormula
    {
        public AssignFormula()
            : base()
        {
            NodeType.Value = FormulaType.Assign;
        }

        public override Formula Accept(IFormulaVisitor visitor)
        {
            throw new NotImplementedException();
        }

        protected override void AppendMethodTo(Node property, StringBuilder sb)
        {
            if (Method.Value == null)
            {
                sb.Append("\"");
                sb.Append(Method.Name);
                sb.Append("\": ");
                sb.Append("\"=\"");
            }
            else
            {
                Method.AppendTo(sb);
            }
        }
    }
}
