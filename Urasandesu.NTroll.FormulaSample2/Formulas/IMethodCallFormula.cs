using System.Collections.ObjectModel;
using Urasandesu.NAnonym.ILTools;

namespace Urasandesu.NTroll.FormulaSample2.Formulas
{
    public interface IMethodCallFormula : IFormula
    {
        IFormula Instance { get; }
        IMethodDeclaration Method { get; }
        ReadOnlyCollection<IFormula> Arguments { get; }
    }
}
