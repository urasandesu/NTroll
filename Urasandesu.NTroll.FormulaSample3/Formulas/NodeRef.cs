using System;
using System.Collections.Generic;
using System.Text;
using Urasandesu.NAnonym.Linq;

namespace Urasandesu.NTroll.FormulaSample3.Formulas
{
    public abstract class NodeRef : Node, INodeRef
    {
        protected internal NodeRef()
            : this(default(INodeRef), default(string), default(INodeRefCollection<INode, INodeRef>))
        {
        }

        protected internal NodeRef(INodeRef parent, string name, INodeRefCollection<INode, INodeRef> children)
            : base(default(INode), default(string), default(INodeCollection<INode>))
        {
            Parent = parent;
            Name = name;
            Children = children;
        }

        INodeRef parent;
        public new INodeRef Parent
        {
            get { return parent; }
            set
            {
                parent = value;
                base.Parent = parent;
            }
        }

        public new string Name 
        { 
            get { return base.Name; } 
            set { base.Name = value; } 
        }

        INodeRefCollection<INode, INodeRef> childrenRef;
        public new INodeRefCollection<INode, INodeRef> Children
        {
            get { return childrenRef; }
            set
            {
                childrenRef = value;
                base.Children = new NodeCollection<INode>(childrenRef.Cast<INode>(), this);
            }
        }

        INode pinned;
        public INode Pin()
        {
            DoIfFirstHierarchy(() =>
            {
                var pinnedParent = Parent == null ? default(INode) : Parent.Pin();
                var pinnedChildren = Children == null ? default(INodeCollection<INode>) : Children.Pin();
                pinned = Pin(pinnedParent, pinnedChildren);
            });
            return pinned;
        }

        protected abstract INode Pin(INode pinnedParent, INodeCollection<INode> pinnedChildren);
    }
}
