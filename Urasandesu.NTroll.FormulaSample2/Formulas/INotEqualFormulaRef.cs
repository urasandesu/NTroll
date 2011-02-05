
namespace Urasandesu.NTroll.FormulaSample2.Formulas
{
    public interface INotEqualFormulaRef : INotEqualFormula, IBinaryFormulaRef
    {
        new INotEqualFormula Establish();
    }
}
