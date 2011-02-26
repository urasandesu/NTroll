using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Urasandesu.NAnonym.ILTools;

namespace Urasandesu.NTroll.FormulaSample5.Formulas
{
    public partial class NewArrayInitFormula : Formula
    {
        public NewArrayInitFormula()
            : base()
        {
            NodeType = NodeType.NewArrayInit;
            Formulas = new FormulaCollection<Formula>();
        }

        FormulaCollection<Formula> formulas;
        public FormulaCollection<Formula> Formulas 
        { 
            get { return formulas; } 
            set { formulas = CheckCanModify(value); } 
        }


        public override Formula Accept(IFormulaVisitor visitor)
        {
            return visitor.Visit(this);
        }


        protected override Formula PinCore()
        {
            Formulas = (FormulaCollection<Formula>)Formula.Pin(Formulas);
            return base.PinCore();
        }


        public override void AppendTo(StringBuilder sb)
        {
            sb.Append("{");
            base.AppendTo(sb);
            sb.Append(", ");
            sb.Append("\"Formulas\": ");
            if (Formulas == null)
            {
                sb.Append("null");
            }
            else
            {
                Formulas.AppendTo(sb);
            }
            sb.Append("}");
        }
    }
}

