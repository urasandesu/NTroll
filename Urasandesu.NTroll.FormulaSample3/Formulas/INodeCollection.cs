using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Collections.ObjectModel;

namespace Urasandesu.NTroll.FormulaSample3.Formulas
{
    public interface INodeCollection<TNode> : INode, IList<TNode>, ICollection<TNode>, IEnumerable<TNode>, IList, ICollection, IEnumerable where TNode : INode
    {
        new TNode this[int index] { get; set; }
    }
}
