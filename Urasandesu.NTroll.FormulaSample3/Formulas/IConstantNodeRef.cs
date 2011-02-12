using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Urasandesu.NTroll.FormulaSample3.Formulas
{
    public interface IConstantNodeRef<TValue> : IConstantNode<TValue>, INodeRef
    {
        new TValue Value { get; set; }
    }
}
