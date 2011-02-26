using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Urasandesu.NAnonym.ILTools;

namespace Urasandesu.NTroll.FormulaSample5.Formulas
{
    public partial class NewFormula : Formula
    {
        public NewFormula()
            : base()
        {
            NodeType = NodeType.New;
            Constructor = default(IConstructorDeclaration);
            Arguments = new FormulaCollection<Formula>();
        }

        IConstructorDeclaration constructor;
        public IConstructorDeclaration Constructor 
        { 
            get { return constructor; } 
            set { constructor = CheckCanModify(value); } 
        }
        FormulaCollection<Formula> arguments;
        public FormulaCollection<Formula> Arguments 
        { 
            get { return arguments; } 
            set { arguments = CheckCanModify(value); } 
        }


        public override Formula Accept(IFormulaVisitor visitor)
        {
            return visitor.Visit(this);
        }


        protected override Formula PinCore()
        {
            Arguments = (FormulaCollection<Formula>)Formula.Pin(Arguments);
            return base.PinCore();
        }


        public override void AppendTo(StringBuilder sb)
        {
            sb.Append("{");
            base.AppendTo(sb);
            sb.Append(", ");
            sb.Append("\"Constructor\": ");
            AppendValueTo(Constructor, sb);
            sb.Append(", ");
            sb.Append("\"Arguments\": ");
            if (Arguments == null)
            {
                sb.Append("null");
            }
            else
            {
                Arguments.AppendTo(sb);
            }
            sb.Append("}");
        }
    }
}

