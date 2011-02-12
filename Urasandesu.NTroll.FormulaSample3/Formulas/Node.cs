using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Urasandesu.NAnonym;

namespace Urasandesu.NTroll.FormulaSample3.Formulas
{
    public abstract class Node : INode
    {
        protected internal Node(INode parent, string name, INodeCollection<INode> children)
        {
            Parent = parent;
            Name = name;
            Children = children;
        }

        public INode Parent { get; protected set; }
        public string Name { get; protected set; }
        public INodeCollection<INode> Children { get; protected set; }

        public abstract void ContentToString(StringBuilder sb);

        int hierarchy;
        protected void DoIfFirstHierarchy(Action ifTrue)
        {
            DoIfFirstHierarchy(ifTrue, () => { });
        }

        protected void DoIfFirstHierarchy(Action ifTrue, Action ifFalse)
        {
            try
            {
                if (hierarchy++ == 0)
                {
                    ifTrue();
                }
                else
                {
                    ifFalse();
                }
            }
            finally
            {
                hierarchy--;
            }
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("{");
            sb.Append("\"HashCode\": ");
            sb.Append(GetHashCode());
            sb.Append(", ");
            sb.Append("\"Parent\": ");
            DoIfFirstHierarchy(() => sb.Append(Parent.NullableToString()), () => sb.Append("\"Abbreviated ...\""));
            foreach (var node in Children)
            {
                sb.Append(", ");
                if (string.IsNullOrEmpty(node.Name))
                {
                    sb.Append(node);
                }
                else
                {
                    sb.Append("\"");
                    sb.Append(node.Name);
                    sb.Append("\": ");
                    node.ContentToString(sb);
                }
            }
            sb.Append("}");
            return sb.ToString();
        }
    }
}
