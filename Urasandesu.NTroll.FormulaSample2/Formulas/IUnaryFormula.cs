using Urasandesu.NAnonym.ILTools;

namespace Urasandesu.NTroll.FormulaSample2.Formulas
{
    public interface IUnaryFormula : IFormula
    {
        IMethodDeclaration Method { get; }
        IFormula Operand { get; }
    }
}
