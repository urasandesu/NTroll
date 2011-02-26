using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Urasandesu.NAnonym.ILTools;

namespace Urasandesu.NTroll.FormulaSample5.Formulas
{
    public partial class CallFormula : Formula
    {
        public CallFormula()
            : base()
        {
            NodeType = NodeType.Call;
            Instance = default(Formula);
            Method = default(IMethodDeclaration);
            Arguments = new FormulaCollection<Formula>();
        }

		public const string NameOfInstance = "Instance";
        Formula instance;
        public Formula Instance 
        { 
            get { return instance; } 
            set { instance = CheckCanModify(value); OnPropertyChanged(NameOfInstance); } 
        }
		public const string NameOfMethod = "Method";
        IMethodDeclaration method;
        public IMethodDeclaration Method 
        { 
            get { return method; } 
            set { method = CheckCanModify(value); OnPropertyChanged(NameOfMethod); } 
        }
		public const string NameOfArguments = "Arguments";
        FormulaCollection<Formula> arguments;
        public FormulaCollection<Formula> Arguments 
        { 
            get { return arguments; } 
            set { arguments = CheckCanModify(value); OnPropertyChanged(NameOfArguments); } 
        }


        public override Formula Accept(IFormulaVisitor visitor)
        {
            return visitor.Visit(this);
        }


        protected override Formula PinCore()
        {
            Instance = Formula.Pin(Instance);
            Arguments = (FormulaCollection<Formula>)Formula.Pin(Arguments);
            return base.PinCore();
        }


        public override void AppendTo(StringBuilder sb)
        {
            sb.Append("{");
            base.AppendTo(sb);
            sb.Append(", ");
            sb.Append("\"");
            sb.Append(NameOfInstance);
            sb.Append("\": ");
            if (Instance == null)
            {
                sb.Append("null");
            }
            else
            {
                Instance.AppendTo(sb);
            }
            sb.Append(", ");
            sb.Append("\"");
            sb.Append(NameOfMethod);
            sb.Append("\": ");
            AppendValueTo(Method, sb);
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
                Arguments.AppendTo(sb);
            }
            sb.Append("}");
        }
    }
}

