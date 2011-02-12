using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Urasandesu.NTroll.FormulaSample3.Formulas
{
    public class ConstantNodeRef<TValue> : NodeRef, IConstantNodeRef<TValue>
    {
        public ConstantNodeRef(string name)
            : this(name, default(TValue))
        {
        }

        public ConstantNodeRef(string name, TValue value)
            : base(default(INodeRef), name, default(INodeRefCollection<INode, INodeRef>))
        {
            Value = value;
        }

        public TValue Value { get; set; }

        public override void ContentToString(StringBuilder sb)
        {
            this.DumpToString(sb);
        }

        protected override INode Pin(INode pinnedParent, INodeCollection<INode> pinnedChildren)
        {
            return new ConstantNode<TValue>(Name, Value);
        }
    }
}
