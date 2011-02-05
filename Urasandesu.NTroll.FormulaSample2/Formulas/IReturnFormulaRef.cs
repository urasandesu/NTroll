
namespace Urasandesu.NTroll.FormulaSample2.Formulas
{
    public interface IReturnFormulaRef : IReturnFormula, IFormulaRef
    {
        new IFormulaRef Object { get; set; }
        new IReturnFormula Establish();
    }
}
