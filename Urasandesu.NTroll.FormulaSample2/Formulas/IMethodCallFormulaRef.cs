using System.Collections.ObjectModel;
using Urasandesu.NAnonym.ILTools;

namespace Urasandesu.NTroll.FormulaSample2.Formulas
{
    public interface IMethodCallFormulaRef : IMethodCallFormula, IFormulaRef
    {
        new IFormulaRef Instance { get; set; }
        new IMethodDeclaration Method { get; set; }
        new Collection<IFormulaRef> Arguments { get; set; }
        new IMethodCallFormula Establish();
    }
}
