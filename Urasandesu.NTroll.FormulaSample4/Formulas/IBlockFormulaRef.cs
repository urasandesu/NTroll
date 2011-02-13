using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Urasandesu.NTroll.FormulaSample4.Formulas
{
    public interface IBlockFormulaRef : IBlockFormula, INonterminalRef
    {
        new IBlockFormulaRef ParentBlock { get; set; }
        new IBlockFormulaRef ChildBlocks { get; set; }
        new INonterminalRef Variables { get; set; }
        new INonterminalRef Formulas { get; set; }
        new INonterminalRef Result { get; set; }
        new IBlockFormula Pin();
    }
}
