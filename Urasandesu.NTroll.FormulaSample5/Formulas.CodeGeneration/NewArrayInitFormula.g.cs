using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Urasandesu.NAnonym.ILTools;
using System.ComponentModel;

namespace Urasandesu.NTroll.FormulaSample5.Formulas
{
    public partial class NewArrayInitFormula : Formula
    {
        public NewArrayInitFormula()
            : base()
        {
            NodeType = NodeType.NewArrayInit;
            Formulas = new FormulaCollection<Formula>();
            Initialize();
        }

        public const string NameOfFormulas = "Formulas";
        FormulaCollection<Formula> formulas;
        public FormulaCollection<Formula> Formulas 
        { 
            get { return formulas; } 
            set 
            {
                SetFormula(NameOfFormulas, value, ref formulas);
            }
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
            sb.Append("\"");
            sb.Append(NameOfFormulas);
            sb.Append("\": ");
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

