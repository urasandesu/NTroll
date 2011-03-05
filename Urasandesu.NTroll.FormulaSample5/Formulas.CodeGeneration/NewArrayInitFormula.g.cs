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

        protected override void InitializeForCodeGeneration()
        {
            base.InitializeForCodeGeneration();
            NodeType = NodeType.NewArrayInit;
            Formulas = new FormulaCollection<Formula>();
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


        protected override void PinCore()
        {
            Formula.Pin(Formulas);
            base.PinCore();
        }


        public override void AppendTo(StringBuilder sb)
        {
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
                Formulas.AppendWithBracketTo(sb);
            }
        }
    }
}

