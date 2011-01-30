using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Text;
using Urasandesu.NAnonym;
using Urasandesu.NAnonym.ILTools;
using Urasandesu.NAnonym.Mixins.System;
using Urasandesu.NAnonym.Mixins.System.Reflection;

namespace Urasandesu.NTroll.FormulaSample.Formulas
{
    public abstract class UnaryFormula : Formula
    {
        protected internal UnaryFormula(NodeType nodeType, ITypeDeclaration type, IMethodDeclaration method, Formula operand)
            : base(nodeType, type)
        {
            Method = method;
            Operand = operand;
        }

        public IMethodDeclaration Method { get; private set; }
        public Formula Operand { get; private set; }

        protected override void AppendToString(StringBuilder sb)
        {
            base.AppendToString(sb);
            sb.Append(", ");
            sb.Append("\"Method\": ");
            sb.Append("\"");
            sb.Append(Method.NullableToString());
            sb.Append("\"");
            sb.Append(", ");
            sb.Append("\"Operand\": ");
            sb.Append(Operand.NullableToString());
        }
    }
}
