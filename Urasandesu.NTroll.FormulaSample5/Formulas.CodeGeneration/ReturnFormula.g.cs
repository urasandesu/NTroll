using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Urasandesu.NAnonym.ILTools;
using System.ComponentModel;

namespace Urasandesu.NTroll.FormulaSample5.Formulas
{
    public partial class ReturnFormula : Formula
    {

        protected override void InitializeForCodeGeneration()
        {
            base.InitializeForCodeGeneration();
            NodeType = NodeType.Return;
            Body = default(Formula);
        }

        public const string NameOfBody = "Body";
        Formula body;
        public Formula Body 
        { 
            get { return body; } 
            set 
            {
                SetFormula(NameOfBody, value, ref body);
            }
        }


        public override Formula Accept(IFormulaVisitor visitor)
        {
            return visitor.Visit(this);
        }


        protected override void PinCore()
        {
            Formula.Pin(Body);
            base.PinCore();
        }


        public override void AppendTo(StringBuilder sb)
        {
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
                Body.AppendWithBracketTo(sb);
            }
        }
    }
}

