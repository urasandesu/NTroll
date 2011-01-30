using System;
using System.Collections.ObjectModel;
using System.Text;
using Urasandesu.NAnonym;
using Urasandesu.NAnonym.ILTools;
using Urasandesu.NAnonym.Linq;

namespace Urasandesu.NTroll.FormulaSample.Formulas
{
    public class MethodCallFormula : Formula
    {
        protected internal MethodCallFormula(Formula instance, IMethodDeclaration method, ReadOnlyCollection<Formula> arguments)
            : base(NodeType.Call, method.ReturnType)
        {
            Instance = instance;
            Method = method;
            Arguments = arguments;
        }

        public Formula Instance { get; private set; }
        public IMethodDeclaration Method { get; private set; }
        public ReadOnlyCollection<Formula> Arguments { get; private set; }

        public override Formula Accept(IFormulaVisitor visitor)
        {
            throw new NotImplementedException();
        }

        protected override void AppendToString(StringBuilder sb)
        {
            base.AppendToString(sb);
            sb.Append(", ");
            sb.Append("\"Instance\": ");
            sb.Append(Instance.NullableToString());
            sb.Append(", ");
            sb.Append("\"Method\": ");
            sb.Append("\"");
            sb.Append(Method.NullableToString());
            sb.Append("\"");
            sb.Append(", ");
            sb.Append("\"Arguments\": ");
            sb.Append(Arguments.NullableJoinToString("[", ", ", _ => _.NullableToString(), "]"));
        }
    }
}
