using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Urasandesu.NAnonym.ILTools;
using System.ComponentModel;

namespace Urasandesu.NTroll.FormulaSample5.Formulas
{
    public abstract partial class UnaryFormula : Formula
    {

        protected override void InitializeForCodeGeneration()
        {
            base.InitializeForCodeGeneration();
            NodeType = NodeType.None;
            Method = default(IMethodDeclaration);
            Operand = default(Formula);
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
        public abstract string MethodToStringValueIfDefault { get; }
        public const string NameOfOperand = "Operand";
        Formula operand;
        public Formula Operand 
        { 
            get { return operand; } 
            set 
            {
                SetFormula(NameOfOperand, value, ref operand);
            }
        }


        protected override void PinCore()
        {
            Formula.Pin(Operand);
            base.PinCore();
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
                Operand.AppendWithBracketTo(sb);
            }
        }
    }
}

