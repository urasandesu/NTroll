using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Urasandesu.NAnonym.ILTools;
using System.ComponentModel;

namespace Urasandesu.NTroll.FormulaSample5.Formulas
{
    public abstract partial class BinaryFormula : Formula
    {

        protected override void InitializeForCodeGeneration()
        {
            base.InitializeForCodeGeneration();
            NodeType = NodeType.None;
            Left = default(Formula);
            Method = default(IMethodDeclaration);
            Right = default(Formula);
        }

        public const string NameOfLeft = "Left";
        Formula left;
        public Formula Left 
        { 
            get { return left; } 
            set 
            {
                SetFormula(NameOfLeft, value, ref left);
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
        public abstract string MethodToStringValueIfDefault { get; }
        public const string NameOfRight = "Right";
        Formula right;
        public Formula Right 
        { 
            get { return right; } 
            set 
            {
                SetFormula(NameOfRight, value, ref right);
            }
        }


        protected override void PinCore()
        {
            Formula.Pin(Left);
            Formula.Pin(Right);
            base.PinCore();
        }


        public override void AppendTo(StringBuilder sb)
        {
            base.AppendTo(sb);
            sb.Append(", ");
            sb.Append("\"");
            sb.Append(NameOfLeft);
            sb.Append("\": ");
            if (Left == null)
            {
                sb.Append("null");
            }
            else
            {
                Left.AppendWithBracketTo(sb);
            }
            sb.Append(", ");
            sb.Append("\"");
            sb.Append(NameOfMethod);
            sb.Append("\": ");
            AppendValueTo(Method, sb, MethodToStringValueIfDefault);
            sb.Append(", ");
            sb.Append("\"");
            sb.Append(NameOfRight);
            sb.Append("\": ");
            if (Right == null)
            {
                sb.Append("null");
            }
            else
            {
                Right.AppendWithBracketTo(sb);
            }
        }
    }
}

