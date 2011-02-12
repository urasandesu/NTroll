using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Urasandesu.NTroll.FormulaSample3.Formulas
{
    public interface IConstantNode<TValue> : INode
    {
        TValue Value { get; }
    }
}
