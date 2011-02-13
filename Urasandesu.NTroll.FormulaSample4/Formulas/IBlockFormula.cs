using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using Urasandesu.NAnonym.ILTools;

namespace Urasandesu.NTroll.FormulaSample4.Formulas
{
    public interface IBlockFormula : INonterminal
    {
        IBlockFormula ParentBlock { get; }
        IBlockFormula ChildBlocks { get; }
        INonterminal Variables { get; }
        INonterminal Formulas { get; }
        INonterminal Result { get; }
    }
}
