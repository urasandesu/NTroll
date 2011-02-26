using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Urasandesu.NAnonym.ILTools;

namespace Urasandesu.NTroll.FormulaSample5.Formulas
{
    public abstract partial class UnaryFormula : Formula
    {
        public UnaryFormula()
            : base()
        {
            NodeType = NodeType.None;
            Method = default(IMethodDeclaration);
            Operand = default(Formula);
        }

		public const string NameOfMethod = "Method";
        IMethodDeclaration method;
        public IMethodDeclaration Method 
        { 
            get { return method; } 
            set { method = CheckCanModify(value); OnPropertyChanged(NameOfMethod); } 
        }
        public abstract string MethodToStringValueIfDefault { get; }
		public const string NameOfOperand = "Operand";
        Formula operand;
        public Formula Operand 
        { 
            get { return operand; } 
            set { operand = CheckCanModify(value); OnPropertyChanged(NameOfOperand); } 
        }


        protected override Formula PinCore()
        {
            Operand = Formula.Pin(Operand);
            return base.PinCore();
        }


        public override void AppendTo(StringBuilder sb)
        {
            base.AppendTo(sb);
            sb.Append(", ");
            sb.Append("\"");
            sb.Append(NameOfMethod);
            sb.Append("\": ");
            AppendValueTo(Method, sb, MethodToStringValueIfDefault);
            sb.Append(", ");
            sb.Append("\"");
            sb.Append(NameOfOperand);
            sb.Append("\": ");
            if (Operand == null)
            {
                sb.Append("null");
            }
            else
            {
                Operand.AppendTo(sb);
            }
        }
    }
}

