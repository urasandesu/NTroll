using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using Urasandesu.NAnonym.Linq;

namespace Urasandesu.NTroll.FormulaSample3.Formulas
{
    public class NodeRefCollection<TNode, TNodeRef> : Collection<TNodeRef>, INodeRefCollection<TNode, TNodeRef>
        where TNode : INode
        where TNodeRef : TNode, INodeRef
    {
        public NodeRefCollection()
            : base()
        {
        }

        public NodeRefCollection(IList<TNodeRef> list, INodeRef parent)
            : base(list)
        {
            Parent = parent;
        }

        public INodeRef Parent { get; set; }
        public string Name { get; set; }
        public INodeRefCollection<INode, INodeRef> Children { get; set; }

        INode INode.Parent
        {
            get { throw new NotImplementedException(); }
        }

        INodeCollection<INode> INode.Children
        {
            get { throw new NotImplementedException(); }
        }

        public void ContentToString(StringBuilder sb)
        {
            throw new NotImplementedException();
        }

        public INodeCollection<TNode> Pin()
        {
            return new NodeCollection<TNode>(this.Select(_ => _.Pin()).ToList().Cast<TNode>(), Parent.Pin());
        }

        INode IImmutable<INode>.Pin()
        {
            return Pin();
        }
    }
}
