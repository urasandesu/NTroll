using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Urasandesu.NTroll.FormulaSample3.Formulas
{
    public interface IBlockFormulaRef : IBlockFormula, IFormulaRef
    {
        new IBlockFormulaRef ParentBlock { get; set; }
        new NodeRefCollection<IBlockFormula, IBlockFormulaRef> ChildBlocks { get; set; }
        new NodeRefCollection<IFormula, IFormulaRef> Variables { get; set; }
        new NodeRefCollection<IFormula, IFormulaRef> Formulas { get; set; }
        new IFormulaRef Result { get; set; }
    }
}
