using System;
using System.Collections.ObjectModel;
using System.Text;
using Urasandesu.NAnonym;
using Urasandesu.NAnonym.ILTools;
using Urasandesu.NAnonym.Linq;

namespace Urasandesu.NTroll.FormulaSample.Formulas
{
    public class NewFormula : Formula
    {
        protected internal NewFormula(IConstructorDeclaration constructor, ReadOnlyCollection<Formula> arguments, ReadOnlyCollection<IMemberDeclaration> members)
            : base(NodeType.New, constructor.DeclaringType)
        {
            Constructor = constructor;
            Arguments = arguments;
            Members = members;
        }

        public IConstructorDeclaration Constructor { get; private set; }
        public ReadOnlyCollection<Formula> Arguments { get; private set; }
        public ReadOnlyCollection<IMemberDeclaration> Members { get; private set; }

        public override Formula Accept(IFormulaVisitor visitor)
        {
            throw new NotImplementedException();
        }

        protected override void AppendToString(StringBuilder sb)
        {
            base.AppendToString(sb);
            sb.Append(", ");
            sb.Append("\"Constructor\": ");
            sb.Append("\"");
            sb.Append(Constructor.NullableToString());
            sb.Append("\"");
            sb.Append(", ");
            sb.Append("\"Parameters\": ");
            sb.Append(Arguments.NullableJoinToString("[", ", ", _ => _.NullableToString(), "]"));
            sb.Append(", ");
            sb.Append("\"Members\": ");
            sb.Append(Members.NullableJoinToString("[", ", ", _ => _.NullableToString(), "]"));
        }
    }
}
