using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Urasandesu.NAnonym.ILTools;

namespace Urasandesu.NTroll.FormulaSample5.Formulas
{
    public partial class AddFormula : BinaryFormula
    {
        public AddFormula()
            : base()
        {
			NodeType = NodeType.Add;
        }

        public override Formula Accept(IFormulaVisitor visitor)
        {
			return visitor.Visit(this);
        }
		
        public override void AppendTo(StringBuilder sb)
		{
			sb.Append("{");
			base.AppendTo(sb);
			sb.Append("}");
		}
    }
}

