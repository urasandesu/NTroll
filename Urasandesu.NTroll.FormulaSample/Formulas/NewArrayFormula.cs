using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Text;
using Urasandesu.NAnonym;
using Urasandesu.NAnonym.ILTools;
using Urasandesu.NAnonym.Mixins.System;
using Urasandesu.NAnonym.Mixins.System.Reflection;
using Urasandesu.NAnonym.Linq;

namespace Urasandesu.NTroll.FormulaSample.Formulas
{
    public abstract class NewArrayFormula : Formula
    {
        protected internal NewArrayFormula(NodeType nodeType, ReadOnlyCollection<Formula> formulas)
            : base(nodeType, formulas.GetType().GetGenericArguments()[0].MakeArrayType().ToTypeDecl())
        {
            Formulas = formulas;
        }

        public ReadOnlyCollection<Formula> Formulas { get; private set; }

        protected override void AppendToString(StringBuilder sb)
        {
            base.AppendToString(sb);
            sb.Append(", ");
            sb.Append("\"Formulas\": ");
            sb.Append(Formulas.NullableJoinToString("[", ", ", _ => _.NullableToString(), "]"));
        }
    }
}
