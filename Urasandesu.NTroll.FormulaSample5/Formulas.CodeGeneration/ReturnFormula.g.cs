using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Urasandesu.NAnonym.ILTools;

namespace Urasandesu.NTroll.FormulaSample5.Formulas
{
    public partial class ReturnFormula : Formula
    {
        public ReturnFormula()
            : base()
        {
            NodeType = NodeType.Return;
            Body = default(Formula);
        }

		public const string NameOfBody = "Body";
        Formula body;
        public Formula Body 
        { 
            get { return body; } 
            set { body = CheckCanModify(value); OnPropertyChanged(NameOfBody); } 
        }


        public override Formula Accept(IFormulaVisitor visitor)
        {
            return visitor.Visit(this);
        }


        protected override Formula PinCore()
        {
            Body = Formula.Pin(Body);
            return base.PinCore();
        }


        public override void AppendTo(StringBuilder sb)
        {
            sb.Append("{");
            base.AppendTo(sb);
            sb.Append(", ");
            sb.Append("\"");
            sb.Append(NameOfBody);
            sb.Append("\": ");
            if (Body == null)
            {
                sb.Append("null");
            }
            else
            {
                Body.AppendTo(sb);
            }
            sb.Append("}");
        }
    }
}

