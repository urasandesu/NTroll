using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Urasandesu.NAnonym.ILTools;

namespace Urasandesu.NTroll.FormulaSample5.Formulas
{
    public partial class ConditionalFormula : Formula
    {
        public ConditionalFormula()
            : base()
        {
			NodeType = NodeType.Conditional;
			Test = default(Formula);
			IfTrue = default(Formula);
			IfFalse = default(Formula);
        }

        Formula test;
        public Formula Test 
		{ 
			get { return test; } 
			set { test = CheckCanModify(value); } 
		}
        Formula ifTrue;
        public Formula IfTrue 
		{ 
			get { return ifTrue; } 
			set { ifTrue = CheckCanModify(value); } 
		}
        Formula ifFalse;
        public Formula IfFalse 
		{ 
			get { return ifFalse; } 
			set { ifFalse = CheckCanModify(value); } 
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
			sb.Append("\"Test\": ");
            if (Test == null)
            {
                sb.Append("null");
            }
            else
            {
                Test.AppendTo(sb);
            }
			sb.Append(", ");
			sb.Append("\"IfTrue\": ");
            if (IfTrue == null)
            {
                sb.Append("null");
            }
            else
            {
                IfTrue.AppendTo(sb);
            }
			sb.Append(", ");
			sb.Append("\"IfFalse\": ");
            if (IfFalse == null)
            {
                sb.Append("null");
            }
            else
            {
                IfFalse.AppendTo(sb);
            }
			sb.Append("}");
		}
    }
}

