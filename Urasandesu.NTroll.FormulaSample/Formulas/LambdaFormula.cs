using System;
using System.Collections.ObjectModel;
using System.Text;
using Urasandesu.NAnonym;
using Urasandesu.NAnonym.Linq;

namespace Urasandesu.NTroll.FormulaSample.Formulas
{
    public class LambdaFormula : Formula
    {
        protected internal LambdaFormula(Formula body, ReadOnlyCollection<Formula> parameters)
            : base(NodeType.Lambda, body.Type)
        {
            Body = body;
            Parameters = parameters;
        }

        public Formula Body { get; private set; }
        public ReadOnlyCollection<Formula> Parameters { get; private set; }

        public override Formula Accept(IFormulaVisitor visitor)
        {
            throw new NotImplementedException();
        }

        protected override void AppendToString(StringBuilder sb)
        {
            base.AppendToString(sb);
            sb.Append(", ");
            sb.Append("\"Body\": ");
            sb.Append(Body.NullableToString());
            sb.Append(", ");
            sb.Append("\"Parameters\": ");
            sb.Append(Parameters.NullableJoinToString("[", ", ", _ => _.NullableToString(), "]"));
        }
    }
}
