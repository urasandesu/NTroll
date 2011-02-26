using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Urasandesu.NAnonym.ILTools;

namespace Urasandesu.NTroll.FormulaSample5.Formulas
{
    public abstract partial class BinaryFormula : Formula
    {
        public BinaryFormula()
            : base()
        {
            NodeType = NodeType.None;
            Left = default(Formula);
            Method = default(IMethodDeclaration);
            Right = default(Formula);
        }

        Formula left;
        public Formula Left 
        { 
            get { return left; } 
            set { left = CheckCanModify(value); } 
        }
        IMethodDeclaration method;
        public IMethodDeclaration Method 
        { 
            get { return method; } 
            set { method = CheckCanModify(value); } 
        }
        public abstract string MethodToStringValueIfDefault { get; }
        Formula right;
        public Formula Right 
        { 
            get { return right; } 
            set { right = CheckCanModify(value); } 
        }


        protected override Formula PinCore()
        {
            Left = Formula.Pin(Left);
            Right = Formula.Pin(Right);
            return base.PinCore();
        }


        public override void AppendTo(StringBuilder sb)
        {
            base.AppendTo(sb);
            sb.Append(", ");
            sb.Append("\"Left\": ");
            if (Left == null)
            {
                sb.Append("null");
            }
            else
            {
                Left.AppendTo(sb);
            }
            sb.Append(", ");
            sb.Append("\"Method\": ");
            AppendValueTo(Method, sb, MethodToStringValueIfDefault);
            sb.Append(", ");
            sb.Append("\"Right\": ");
            if (Right == null)
            {
                sb.Append("null");
            }
            else
            {
                Right.AppendTo(sb);
            }
        }
    }
}

