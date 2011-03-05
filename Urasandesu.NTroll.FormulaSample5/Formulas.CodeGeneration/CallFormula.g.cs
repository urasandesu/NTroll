using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Urasandesu.NAnonym.ILTools;
using System.ComponentModel;

namespace Urasandesu.NTroll.FormulaSample5.Formulas
{
    public partial class CallFormula : Formula
    {

        protected override void InitializeForCodeGeneration()
        {
            base.InitializeForCodeGeneration();
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
            set 
            {
                SetFormula(NameOfInstance, value, ref instance);
            }
        }
        public const string NameOfMethod = "Method";
        IMethodDeclaration method;
        public IMethodDeclaration Method 
        { 
            get { return method; } 
            set 
            {
                SetValue(NameOfMethod, value, ref method);
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
            Formula.Pin(Instance);
            Formula.Pin(Arguments);
            base.PinCore();
        }


        public override void AppendTo(StringBuilder sb)
        {
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
                Instance.AppendWithBracketTo(sb);
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
                Arguments.AppendWithBracketTo(sb);
            }
        }
    }
}

