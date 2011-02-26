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

        IMethodDeclaration method;
        public IMethodDeclaration Method 
        { 
            get { return method; } 
            set { method = CheckCanModify(value); } 
        }
        public abstract string MethodToStringValueIfDefault { get; }
        Formula operand;
        public Formula Operand 
        { 
            get { return operand; } 
            set { operand = CheckCanModify(value); } 
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
            sb.Append("\"Method\": ");
            AppendValueTo(Method, sb, MethodToStringValueIfDefault);
            sb.Append(", ");
            sb.Append("\"Operand\": ");
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

