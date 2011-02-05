
namespace Urasandesu.NTroll.FormulaSample2.Formulas
{
    public interface IMultiplyFormulaRef : IMultiplyFormula, IBinaryFormulaRef
    {
        new IMultiplyFormula Establish();
    }
}
