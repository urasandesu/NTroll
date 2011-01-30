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
    public abstract class MemberFormula : Formula
    {
        protected internal MemberFormula(NodeType nodeType, ITypeDeclaration type, Formula instance, IMemberDeclaration member)
            : base(nodeType, type)
        {
            Instance = instance;
            Member = member;
        }

        public Formula Instance { get; private set; }
        public IMemberDeclaration Member { get; private set; }

        protected override void AppendToString(StringBuilder sb)
        {
            base.AppendToString(sb);
            sb.Append(", ");
            sb.Append("\"Instance\": ");
            sb.Append(Instance.NullableToString());
            sb.Append(", ");
            sb.Append("\"Member\": ");
            sb.Append("\"");
            sb.Append(Member.NullableToString());
            sb.Append("\"");
        }
    }
}
