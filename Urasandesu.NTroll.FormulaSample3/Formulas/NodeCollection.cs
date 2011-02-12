using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace Urasandesu.NTroll.FormulaSample3.Formulas
{
    public class NodeCollection<TNode> : ReadOnlyCollection<TNode>, INodeCollection<TNode> where TNode : INode
    {
        INode parent;
        public NodeCollection(IList<TNode> list, INode parent)
            : base(list)
        {
            this.parent = parent;
        }

        public INode Parent
        {
            get { return parent; }
        }

        public new TNode this[int index]
        {
            get
            {
                return base[index];
            }
            set
            {
                throw new NotSupportedException();
            }
        }

        public string Name
        {
            get { throw new NotImplementedException(); }
        }

        public INodeCollection<INode> Children
        {
            get { throw new NotImplementedException(); }
        }

        public void ContentToString(StringBuilder sb)
        {
            throw new NotImplementedException();
        }
    }
}
