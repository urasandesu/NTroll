using System;
using System.Collections.ObjectModel;
using System.Text;
using Urasandesu.NAnonym;
using Urasandesu.NAnonym.ILTools;
using Urasandesu.NAnonym.Linq;

namespace Urasandesu.NTroll.FormulaSample2.Formulas
{
    public class MethodCallFormula : Formula, IMethodCallFormula
    {
        protected internal MethodCallFormula(
            IFormula instance, 
            IMethodDeclaration method, 
            ReadOnlyCollection<IFormula> arguments, 
            ITypeDeclaration type, 
            IFormula parent)
            : base(NodeType.Call, type, parent)
        {
            Instance = instance;
            Method = method;
            Arguments = arguments;
        }

        public IFormula Instance { get; protected set; }
        public IMethodDeclaration Method { get; protected set; }
        public ReadOnlyCollection<IFormula> Arguments { get; protected set; }

        public override IFormula Accept(IFormulaVisitor visitor)
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
