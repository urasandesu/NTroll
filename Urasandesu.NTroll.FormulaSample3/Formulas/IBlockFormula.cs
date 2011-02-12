using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Urasandesu.NTroll.FormulaSample3.Formulas
{
    public interface IBlockFormula : IFormula
    {
        IBlockFormula ParentBlock { get; }
        INodeCollection<IBlockFormula> ChildBlocks { get; }
        INodeCollection<IFormula> Variables { get; }
        INodeCollection<IFormula> Formulas { get; }
        IFormula Result { get; }
    }
}
