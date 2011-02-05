using System.Collections.ObjectModel;

namespace Urasandesu.NTroll.FormulaSample2.Formulas
{
    public interface IBlockFormula : IFormula
    {
        IBlockFormula ParentBlock { get; }
        ReadOnlyCollection<IBlockFormula> ChildBlocks { get; }
        ReadOnlyCollection<IFormula> Variables { get; }
        ReadOnlyCollection<IFormula> Formulas { get; }
        IFormula Result { get; }
    }
}
