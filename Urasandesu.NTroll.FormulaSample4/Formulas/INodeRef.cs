using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Urasandesu.NTroll.FormulaSample4.Formulas
{
    public interface INodeRef : INode, IHierarchal<INodeRef>
    {
        new string Name { get; set; }
        new INodeRef Referrers { get; set; }
        INode Pin();
    }
}
