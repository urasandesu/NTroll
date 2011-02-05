
namespace Urasandesu.NTroll.FormulaSample2.Formulas
{
    public interface IAddFormulaRef : IAddFormula, IBinaryFormulaRef
    {
        new IAddFormula Establish();
    }
}
