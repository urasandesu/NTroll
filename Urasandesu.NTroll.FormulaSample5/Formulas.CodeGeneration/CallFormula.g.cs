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

        Formula instance;
        public Formula Instance 
        { 
            get { return instance; } 
            set { instance = CheckCanModify(value); } 
        }
        IMethodDeclaration method;
        public IMethodDeclaration Method 
        { 
            get { return method; } 
            set { method = CheckCanModify(value); } 
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
            Instance = Formula.Pin(Instance);
            Arguments = (FormulaCollection<Formula>)Formula.Pin(Arguments);
            return base.PinCore();
        }


        public override void AppendTo(StringBuilder sb)
        {
            sb.Append("{");
            base.AppendTo(sb);
            sb.Append(", ");
            sb.Append("\"Instance\": ");
            if (Instance == null)
            {
                sb.Append("null");
            }
            else
            {
                Instance.AppendTo(sb);
            }
            sb.Append(", ");
            sb.Append("\"Method\": ");
            AppendValueTo(Method, sb);
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

