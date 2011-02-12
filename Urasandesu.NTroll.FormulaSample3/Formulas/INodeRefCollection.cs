
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using Urasandesu.NAnonym.Linq;

namespace Urasandesu.NTroll.FormulaSample3.Formulas
{
    public interface INodeRefCollection<TNode, TNodeRef> : INodeRef, INodeCollection<TNodeRef>, IImmutable<INodeCollection<TNode>>
        where TNode : INode
        where TNodeRef : TNode, INodeRef
    {
        new INodeCollection<TNode> Pin();
    }
}
