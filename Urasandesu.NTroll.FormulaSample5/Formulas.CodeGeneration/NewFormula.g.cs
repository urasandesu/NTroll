using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Urasandesu.NAnonym.ILTools;
using System.ComponentModel;

namespace Urasandesu.NTroll.FormulaSample5.Formulas
{
    public partial class NewFormula : Formula
    {

        protected override void InitializeForCodeGeneration()
        {
            base.InitializeForCodeGeneration();
            NodeType = NodeType.New;
            Constructor = default(IConstructorDeclaration);
            Arguments = new FormulaCollection<Formula>();
        }

        public const string NameOfConstructor = "Constructor";
        IConstructorDeclaration constructor;
        public IConstructorDeclaration Constructor 
        { 
            get { return constructor; } 
            set 
            {
                SetValue(NameOfConstructor, value, ref constructor);
            }
        }
        public const string NameOfArguments = "Arguments";
        FormulaCollection<Formula> arguments;
        public FormulaCollection<Formula> Arguments 
        { 
            get { return arguments; } 
            set 
            {
                SetFormula(NameOfArguments, value, ref arguments);
            }
        }


        public override Formula Accept(IFormulaVisitor visitor)
        {
            return visitor.Visit(this);
        }


        protected override void PinCore()
        {
            Formula.Pin(Arguments);
            base.PinCore();
        }


        public override void AppendTo(StringBuilder sb)
        {
            base.AppendTo(sb);
            sb.Append(", ");
            sb.Append("\"");
            sb.Append(NameOfConstructor);
            sb.Append("\": ");
            AppendValueTo(Constructor, sb);
            sb.Append(", ");
            sb.Append("\"");
            sb.Append(NameOfArguments);
            sb.Append("\": ");
            if (Arguments == null)
            {
                sb.Append("null");
            }
            else
            {
                Arguments.AppendWithBracketTo(sb);
            }
        }
    }
}

