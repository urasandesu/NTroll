using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Urasandesu.NAnonym;
using Urasandesu.NAnonym.ILTools;
using Urasandesu.NAnonym.Linq;
using System.Collections.ObjectModel;

namespace Urasandesu.NTroll.FormulaSample5.Formulas
{
    public abstract partial class Formula : Node
    {
        public Formula(string name)
            : this(name, default(FormulaType))
        {
        }

        public Formula(string name, FormulaType nodeType)
            : this()
        {
            Name = name;
            NodeType.Value = nodeType;
        }

        IList<Node> properties = new Collection<Node>();
        public IList<Node> Properties { get { return properties; } set { properties = CheckCanModify(value); } }

        Hierarchy hierarchy = new Hierarchy();
        public Hierarchy Hierarchy { get { return hierarchy; } }

        protected override Node PinCore()
        {
            properties = new ReadOnlyCollection<Node>(properties);
            return base.PinCore();
        }

        public abstract Formula Accept(IFormulaVisitor visitor);

        public override void AppendContentTo(StringBuilder sb)
        {
            AppendFormulaContentTo(sb);
        }

        public void AppendFormulaContentTo(StringBuilder sb)
        {
            sb.Append("{");
            Hierarchy.DoIfFirstLevel(() => Referrer.AppendTo(sb), () => sb.Append("\"Abbreviated ...\""));
            for (int propertyIndex = ReferrerIndex + 1; propertyIndex < Properties.Count; propertyIndex++)
            {
                var property = Properties[propertyIndex];
                sb.Append(", ");
                OnPropertyAppending(property, propertyIndex, sb);
            }
            sb.Append("}");
        }

        protected virtual void OnPropertyAppending(Node property, int propertyIndex, StringBuilder sb)
        {
            property.AppendTo(sb);
        }
    }
}
