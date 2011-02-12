using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Urasandesu.NTroll.FormulaSample3.Formulas
{
    public interface INodeRef : INode, IImmutable<INode>
    {
        new INodeRef Parent { get; set; }
        new string Name { get; set; }
        new INodeRefCollection<INode, INodeRef> Children { get; set; }
    }
}
