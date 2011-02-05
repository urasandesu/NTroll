using System.Collections.ObjectModel;

namespace Urasandesu.NTroll.FormulaSample2.Formulas
{
    public interface IBlockFormulaRef : IBlockFormula, IFormulaRef
    {
        new IBlockFormulaRef ParentBlock { get; set; }
        new Collection<IBlockFormulaRef> ChildBlocks { get; set; }
        new Collection<IFormulaRef> Variables { get; set; }
        new Collection<IFormulaRef> Formulas { get; set; }
        new IFormulaRef Result { get; set; }
        new IBlockFormula Establish();
    }
}
