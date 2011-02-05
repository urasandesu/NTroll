
namespace Urasandesu.NTroll.FormulaSample2.Formulas
{
    public interface IVariableFormulaRef : IVariableFormula, IFormulaRef
    {
        new string Name { get; set; }
        new IVariableFormula Establish();
    }
}
