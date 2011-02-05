
namespace Urasandesu.NTroll.FormulaSample2.Formulas
{
    public interface IExclusiveOrFormulaRef : IExclusiveOrFormula, IBinaryFormulaRef
    {
        new IExclusiveOrFormula Establish();
    }
}
