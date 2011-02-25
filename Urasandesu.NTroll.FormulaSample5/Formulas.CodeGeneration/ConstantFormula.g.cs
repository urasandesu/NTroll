using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Urasandesu.NAnonym.ILTools;

namespace Urasandesu.NTroll.FormulaSample5.Formulas
{
    public partial class ConstantFormula : Formula
    {
        public ConstantFormula()
            : base()
        {
			NodeType = NodeType.Constant;
			ConstantValue = default(object);
        }

        object constantValue;
        public object ConstantValue 
		{ 
			get { return constantValue; } 
			set { constantValue = CheckCanModify(value); } 
		}
        public override Formula Accept(IFormulaVisitor visitor)
        {
			return visitor.Visit(this);
        }
		
        public override void AppendTo(StringBuilder sb)
		{
			sb.Append("{");
			base.AppendTo(sb);
			sb.Append(", ");
			sb.Append("\"ConstantValue\": ");
			AppendValueTo(ConstantValue, sb);
			sb.Append("}");
		}
    }
}

